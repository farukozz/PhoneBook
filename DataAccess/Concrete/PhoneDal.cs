using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Abstract;

namespace DataAccess.Concrete
{
    public class PhoneDal:EntityRepository<Phone,PhoneBookContext>,IPhoneDal
    {
    }
}
