using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Core.Utilities.Result;
using Entities.Concrete;
using Business.Constants;
using System.Linq;
using Entities.Dtos;

namespace Business.Concrete
{
    public class PersonService: IPersonService
    {
        private IPersonDal _personDal;
        public PersonService(IPersonDal personDal)
        {
            _personDal = personDal;
        }
        public async Task<IDataResult<Person>> Add(Person person)
        {
            person.FirstName = person.FirstName.ToUpper();
            person.LastName = person.LastName.ToUpper();
            person.Company = person.Company.ToUpper();
            Person addedPerson = await _personDal.Add(person);
            return new SuccessDataResult<Person>(message: Messages.PersonAdded, data: addedPerson);
        }
        public async Task<IDataResult<Person>> Delete(Person person)
        {

            await _personDal.HardDelete(person);
            return new SuccessDataResult<Person>(message: Messages.PersonDeleted);
        }
        public async Task<IDataResult<Person>> Update(Person person)
        {
            await _personDal.Update(person);
            return new SuccessDataResult<Person>(message: Messages.PersonUpdated);
        }
        public async Task<IDataResult<Person>> GetById(int personId)
        {
            return new SuccessDataResult<Person>(await _personDal.Get(x => x.Id == personId));
        }
        public async Task<IDataResult<List<Person>>> GetList()
        {
            var value = await _personDal.GetList();
            return new SuccessDataResult<List<Person>>(value.ToList());
        }
        public async Task<IDataResult<List<PersonPhoneDto>>> GetPersonPhoneDtos()
        {
            var value = await _personDal.PersonPhoneDtos();
            return new SuccessDataResult<List<PersonPhoneDto>>(value.ToList());
        }

    }
}
