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

namespace Business.Concrete
{
    public class PhoneService: IPhoneService
    {
        private IPhoneDal _phoneDal;
        public PhoneService(IPhoneDal phoneDal)
        {
            _phoneDal = phoneDal;
        }
        public async Task<IDataResult<Phone>> Add(Phone phone)
        {

            Phone addedPhone = await _phoneDal.Add(phone);
            return new SuccessDataResult<Phone>(message: Messages.PhoneAdded, data: addedPhone);
        }
        public async Task<IDataResult<Phone>> Delete(Phone phone)
        {

            await _phoneDal.HardDelete(phone);
            return new SuccessDataResult<Phone>(message: Messages.PhoneDeleted);
        }
        public async Task<IDataResult<Phone>> Update(Phone phone)
        {
            await _phoneDal.Update(phone);
            return new SuccessDataResult<Phone>(message: Messages.PhoneUpdated);
        }
        public async Task<IDataResult<Phone>> GetById(int phoneId)
        {
            return new SuccessDataResult<Phone>(await _phoneDal.Get(x => x.Id == phoneId));
        }
        public async Task<IDataResult<List<Phone>>> GetByPersonId(int personId)
        {
            var value = await _phoneDal.GetList(x => x.PersonId == personId);
            return new SuccessDataResult<List<Phone>>(value.ToList());
        }
        public async Task<IDataResult<List<Phone>>> GetList()
        {
            var value = await _phoneDal.GetList();
            return new SuccessDataResult<List<Phone>>(value.ToList());
        }

    }
}
