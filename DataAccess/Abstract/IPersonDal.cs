using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IPersonDal:IEntityRepository<Person>
    {
        Task<List<PersonPhoneDto>> PersonPhoneDtos();
    }
}
