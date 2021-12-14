using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IPersonService
    {
        Task<IDataResult<Person>> GetById(int personId);
        Task<IDataResult<List<Person>>> GetList();
        Task<IDataResult<List<PersonPhoneDto>>> GetPersonPhoneDtos();
        Task<IDataResult<Person>> Add(Person person);
        Task<IDataResult<Person>> Delete(Person person);
        Task<IDataResult<Person>> Update(Person person);
    }
}
