using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class PersonPhoneDto
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public int UserId { get; set; }
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
