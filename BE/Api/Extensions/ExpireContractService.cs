using Microsoft.EntityFrameworkCore;
using static Core.Enums.InvoiceEnums;
using static Core.Enums.LeaseEnums;
using static Core.Enums.RoomEnums;

namespace Api.Extensions;

public class ExpireContractService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public ExpireContractService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
                var expiredInvoices = context.Invoices
                    .Include(i => i.Booking!)
                    .ThenInclude(b => b.Lease!)
                    .Where(i => i.Status == StatusInvoice.Pending && i.DueDate <= DateTime.Now)
                    .ToList();

                var expiredLeases = context.Leases
                    .Include(l => l.LeaseDetails!)
                    .ThenInclude(ld => ld.Room!)
                    .Where(l => l.Status == StatusLease.Active)
                    .ToList();

                foreach (var invoice in expiredInvoices)
                {
                    invoice.Status = StatusInvoice.Overdue;
                    invoice.UpdatedAt = DateTime.Now;
                    invoice.Booking!.Lease!.Status = StatusLease.Canceled;
                    invoice.Booking!.Lease!.UpdatedAt = DateTime.Now;
                }
                var isExpiredLeaseDetails = false;
                foreach (var lease in expiredLeases)
                {
                    var expiredLeaseDetails = lease.LeaseDetails
                        .Where(ld => ld.EndDate <= DateTime.Now);
                    if (expiredLeaseDetails.Any())
                    {
                        isExpiredLeaseDetails = true;
                        foreach (var leaseDetail in expiredLeaseDetails)
                        {
                            leaseDetail.Room!.Condition = ConditionRoom.Available;
                            leaseDetail.Room!.UpdatedAt = DateTime.Now;
                        }
                        if (expiredLeaseDetails.Count() == lease.LeaseDetails.Count())
                        {
                            lease.Status = StatusLease.Expired;
                        }
                    }
                }

                var VisitStat = context.VisitStats
                    .Where(v => v.Year == DateTime.Now.Year && v.Month == DateTime.Now.Month)
                    .FirstOrDefault();
                if (VisitStat == null)
                {
                    context.VisitStats.Add(new VisitStat
                    {
                        Year = DateTime.Now.Year,
                        Month = DateTime.Now.Month,
                    });
                }


                if (expiredInvoices.Any() || expiredLeases.Any() || isExpiredLeaseDetails || VisitStat == null)
                {
                    await context.SaveChangesAsync(stoppingToken);
                }
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}
