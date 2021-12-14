using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using PhoneBook.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.API.AutoMapper
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<RegisterModel, UserForRegisterDto>().ReverseMap();
            CreateMap<PersonModel, Person>().ReverseMap();
        }
    }
}
