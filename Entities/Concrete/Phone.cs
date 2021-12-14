using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Phone:IEntity
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int PersonId { get; set; }

    }
}
