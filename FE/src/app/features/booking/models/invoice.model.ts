import { InvoiceDetail } from './invoice-detail.model';
import { Payment } from './payment.model';

export type Invoice = {
  id: string;
  amount: number;
  dueDate: Date;
  status?: string;
  createdAt?: Date;
  updatedAt?: Date;
  invoiceDetails?: InvoiceDetail[];
  payment?: Payment;
};

export const StatusInvoice: { [key: string]: string } = {
  Pending: 'Đang chờ xử lý',
  Paid: 'Đã thanh toán',
  Overdue: 'Quá hạn',
};
