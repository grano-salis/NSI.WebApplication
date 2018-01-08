﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSI.DC.ContactsRepository;
using Microsoft.EntityFrameworkCore.Internal;
using System.Text.RegularExpressions;
using NSI.DC.AddressRepository;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/contacts")]
    public class ContactsController : Controller
    {
        BLL.Interfaces.IContactsManipulation contactsRepository { get; set; }

        public ContactsController(BLL.Interfaces.IContactsManipulation contactsManipulation)
        {
            this.contactsRepository = contactsManipulation;
        }

        // GET: api/contacts
        [HttpGet]
        public IActionResult Get(int pageSize, int pageNumber, String searchString, String searchColumn, String sortOrder, int caseId)
        {
            return Ok(contactsRepository.GetContacts(pageSize, pageNumber, searchString, searchColumn, sortOrder, caseId));
        }


        // GET: api/contacts/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(contactsRepository.GetContactById(id));
        }

        // GET: api/contacts/forcase/5
        [HttpGet("/forcase/{caseId}")]
        public IActionResult GetByCase(int caseId)
        {
            return Ok(contactsRepository.GetContactsForCase(caseId));
        }


        // POST: api/contacts
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]ContactDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {


                if (model != null)
                {
                    if (ValidationContact(model) == "")
                    {
                        var contact = contactsRepository.CreateContact(model, id);
                        return Ok(contact);
                    }
                    else
                        throw new Exception(ValidationContact(model));

                }

                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //return NoContent();
        }

        // PUT: api/contacts/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ContactDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (ValidationContact(model) == "")
                {
                    var contact = contactsRepository.EditContact(id, model);
                    if (contact)
                    {
                        return Ok(contact);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    throw new Exception(ValidationContact(model));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/contacts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (contactsRepository.DeleteContactById(id))
                {
                    return Ok(id);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string ValidationContact(ContactDto contact)
        {
            String validationMessage = "";

            if (string.IsNullOrEmpty(contact.FirsttName)) validationMessage += " First name is required.";
            if (string.IsNullOrEmpty(contact.LastName)) validationMessage += " Last name is required.";
            if (contact.Phones.Any(x => String.IsNullOrEmpty(x.PhoneNumber))) validationMessage += " All phone number fields should have a value.";
            if (contact.Phones.Select(x => x.PhoneNumber).Distinct().Count() != contact.Phones.Count) validationMessage += " Phone number already exists or the same phone number value is entered several times.";
            if (contact.Emails.Any(x => String.IsNullOrEmpty(x.EmailAddress))) validationMessage += " All email fields should have a value.";
            if (contact.Emails.Select(x => x.EmailAddress).Distinct().Count() != contact.Emails.Count) validationMessage += " Email already exists or the same email address is enetered several times.";
            if (!contact.Emails.All(x => Regex.IsMatch(x.EmailAddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))) validationMessage += "\n Email is not in correct format. Example: someone@domain.com.";
            if (!Regex.IsMatch(contact.FirsttName, @"^[a-zA-Z]+$")) validationMessage += " First name should contain letters only.";
            if (!Regex.IsMatch(contact.LastName, @"^[a-zA-Z]+$")) validationMessage += " Last name should contain letters only.";

            foreach (string p in contact.Phones.Select(x => x.PhoneNumber))
            {
                Int64 result;
                if (!Int64.TryParse(p, out result)) validationMessage += " Phone number " + p + " should contain numbers only.";
            }

            return validationMessage;
        }
    }
}

