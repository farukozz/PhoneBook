using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IPhoneService
    {
        Task<IDataResult<Phone>> GetById(int phoneId);
        Task<IDataResult<List<Phone>>> GetByPersonId(int personId);
        Task<IDataResult<List<Phone>>> GetList();
        Task<IDataResult<Phone>> Add(Phone phone);
        Task<IDataResult<Phone>> Delete(Phone phone);
        Task<IDataResult<Phone>> Update(Phone phone);
    }
}
