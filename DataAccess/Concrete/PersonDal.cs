using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Entities.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Concrete
{
    public class PersonDal : EntityRepository<Person, PhoneBookContext>, IPersonDal
    {
        public async Task<List<PersonPhoneDto>> PersonPhoneDtos()
        {
            using (var ctx = new PhoneBookContext())
            {
                var query = (from phone in ctx.Phones
                             join person in ctx.Persons on phone.PersonId equals person.Id
                             select new PersonPhoneDto
                             {
                                 UserId = person.UserId,
                                 FirstName = person.FirstName,
                                 LastName = person.LastName,
                                 Company = person.Company,
                                 PersonId = person.Id,
                                 PhoneId = phone.Id,
                                 PhoneNumber = phone.PhoneNumber
                             });
                var list = await query.ToListAsync();
                return list;
            }

          
        }
    }
}
