using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PhoneBook.API.Models;
using Business.Abstract;
using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Logging;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonService _personService;
        private IPhoneService _phoneService;
        private IMapper _mapper;
        private readonly ILogger<PersonController> _logger;
        public PersonController(IPersonService personService, IPhoneService phoneService, IMapper mapper, ILogger<PersonController> logger)
        {
            _personService = personService;
            _phoneService = phoneService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize()]
        [HttpPost(template: "add")]
        public async Task<IActionResult> Add(PersonModel personModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = ModelState });
                }
                Person person = _mapper.Map<PersonModel, Person>(personModel);
                var result = await _personService.Add(person);
                Person addedPerson = result.Data;

                foreach (var phoneNumber in personModel.PhoneNumbers)
                {
                    Phone phone = new Phone();
                    phone.PersonId = addedPerson.Id;
                    phone.PhoneNumber = phoneNumber.PhoneNumber;
                    await _phoneService.Add(phone);

                }
                return Ok(new { Message = "Kişi başarıyla kaydedilmiştir" });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Personel eklerken hata oluştu. Hata: {ex}");
                return BadRequest(new { Message = "Personel eklerken hata oluştu" });
            }

        }

        [Authorize()]
        [HttpPost(template: "update")]
        public async Task<IActionResult> Update(PersonModel personModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = ModelState });
                }
                Person personResult = _personService.GetById(personModel.Id).Result.Data;
                Person updatePerson = _mapper.Map<PersonModel, Person>(personModel);
                if (updatePerson == null)
                {
                    _logger.LogError($"Güncellenecek kişi bulunamadı");
                    return BadRequest(new { Message = "Güncellenecek bulunamadı" });
                }
                updatePerson.Id = personResult.Id;
                var result = await _personService.Update(updatePerson);
                List<Phone> personPhones = _phoneService.GetByPersonId(personResult.Id).Result.Data;
                foreach (var perPhone in personPhones)
                {
                    await _phoneService.Delete(perPhone);
                }

                foreach (var phone in personModel.PhoneNumbers)
                {
                    Phone newPhone = new Phone();
                    newPhone.PersonId = personResult.Id;
                    newPhone.PhoneNumber = phone.PhoneNumber;
                    await _phoneService.Add(newPhone);
                }

                return Ok(new { Message = "Kişi başarıyla güncellenmiştir" });
            }
            catch (Exception ex)
            {

                _logger.LogError($"{personModel.Id} id'li Personel güncellerken hata oluştu. Hata: {ex}");
                return BadRequest(new { Message = "Personel güncellerken hata oluştu" });
            }

        }

        [Authorize()]
        [HttpGet(template: "delete")]
        public async Task<IActionResult> Delete(int personId)
        {
            try
            {
                Person deletedPerson = _personService.GetById(personId).Result.Data;
                if (deletedPerson == null)
                {
                    return BadRequest(new { Message = "Silinecek kişi bulunamadı" });
                }
                var result = await _personService.Delete(deletedPerson);
                List<Phone> phones = _phoneService.GetByPersonId(deletedPerson.Id).Result.Data;

                foreach (var phone in phones)
                {
                    await _phoneService.Delete(phone);

                }
                return Ok(new { Message = "Kişi başarıyla silinmiştir" });
            }
            catch (Exception ex)
            {

                _logger.LogError($"{personId} id'li Personel silerken hata oluştu. Hata: {ex}");
                return BadRequest(new { Message = "Personel silerken hata oluştu" });
            }

        }
        [Authorize()]
        [HttpGet(template: "search")]
        public async Task<IActionResult> Search(int userId, string searchText)
        {
            searchText = searchText.ToUpper();
            var searchArr = searchText.Split(' ');
            var result = await _personService.GetPersonPhoneDtos();
            List<PersonPhoneDto> filterPersons = null;
            if (searchArr.Length == 1)
            {
                filterPersons = result.Data.Where(x => x.UserId == userId && (x.FirstName.Contains(searchText) == true || x.LastName.Contains(searchText) == true || x.Company.Contains(searchText) == true || x.PhoneNumber.Contains(searchText) == true)).ToList();
                return Ok(new { Data = filterPersons });
            }
            else if (searchArr.Length > 1)
            {
                filterPersons = result.Data.Where(x => x.UserId == userId && x.FirstName.Contains(searchArr[0]) == true && x.LastName.Contains(searchArr[1]) == true).ToList();
                return Ok(new { Data = filterPersons });
            }
            return BadRequest(new { Message = "Kayıt bulunamadı" });
        }

        [Authorize()]
        [HttpGet(template: "getlist")]
        public async Task<IActionResult> GetList()
        {
            var resultPerson = await _personService.GetList();
            List<PersonModel> persons = new List<PersonModel>();
            foreach (var person in resultPerson.Data)
            {
                PersonModel personModel = _mapper.Map<Person, PersonModel>(person);
                var resultPhone = await _phoneService.GetList();
                var phoneList = resultPhone.Data.Where(x => x.PersonId == person.Id).ToList();
                personModel.PhoneNumbers = phoneList;
                persons.Add(personModel);
            }
            return Ok(persons);
        }
    }
}