using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record UserDto
    {
        public Guid? Id { get; }
        public int UserCode { get; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string? Address { get; set; }
        public string? GenderName
        {
            get
            {
                return Gender switch
                {
                    (int)Enums.Gender.MALE => "Nam",
                    (int)Enums.Gender.FEMALE => "Nữ",
                    (int)Enums.Gender.OTHER => "Không xác định",
                    _ => "Không xác định",
                };
            }
            set { }
        }
    }
}
