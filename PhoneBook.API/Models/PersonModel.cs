using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entities.Concrete;

namespace PhoneBook.API.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Ad boş geçilemez")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyad boş geçilemez")]
        public string LastName { get; set; }
        public string Company { get; set; }
        [Required(ErrorMessage = "Kullanıcı Id boş geçilemez")]
        public int? UserId { get; set; }
        public List<Phone> PhoneNumbers { get; set; }
    }
}
