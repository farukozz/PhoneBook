using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserService: IUserService
    {
        private IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }


        public async Task<User> Add(User user)
        {
            var addedUser=await _userDal.Add(user);
            return addedUser;
        }

        public async Task<User> GetByMail(string email)
        {
            return await _userDal.Get(filter: x => x.Email == email);
        }
    }
}
