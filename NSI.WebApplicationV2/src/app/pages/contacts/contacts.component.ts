import {Component, OnInit} from '@angular/core';
import {ContactsService} from '../../services/contacts.service';

import {PagerService} from "../../services/pagination.service";
import {FormBuilder, Validators} from "@angular/forms";
import {ValidationService} from "./validation.service";
declare let $: any;

class Contact {
  firsttName: any;
  lastName: any;
  phone: any;
  email: any;
  mobile: any;


  constructor() {
    this.firsttName = '';
    this.lastName = '';
    this.phone = '';
    this.email = '';
    this.mobile = '';
  }
}

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})

export class ContactsComponent implements OnInit {
  contacts: any[];
  allContacts: any[];
  temp_contact: any;
  filterColumn: string;
  filterValue: string;
  private allItems: any[];
  form: any;

  // pager object
  pager: any = {};
  sortNumber: number;
  pagedItems: any[];

  constructor(private contactsService: ContactsService, private pagerService: PagerService,
              private formBuilder: FormBuilder) {
    const _this = this;
    this.filterColumn = 'name';
    this.filterValue = '';
    this.temp_contact = new Contact();
    this.sortNumber = 0;
    this.clearNewForm();
  }

  ngOnInit() {
    const _this = this;
    _this.setPage(1);
  }

  addContact() {
    console.log('add contact');
  }

  editContact(contact: any) {
    this.temp_contact = Object.assign({}, contact);
    const con = Object.assign({}, contact);
    this.editForm(con);
  }

  newContact() {
    this.temp_contact = new Contact();
    this.temp_contact.emailsArray = [];
    this.temp_contact.phones = [];
    this.clearNewForm();
  }

  clearNewForm() {
    this.form = this.formBuilder.group({
      'firstname': ['', [Validators.required, ValidationService.lettersOnlyValidator]],
      'lastname': ['', [Validators.required, ValidationService.lettersOnlyValidator]],
      'email': ['', [Validators.required, ValidationService.emailValidator]],
      'phone': ['', [Validators.required, ValidationService.numbersOnlyValidator]],
      'emails': this.formBuilder.array([]),
      'phones': this.formBuilder.array([])
    });
  }

  editForm(cont: any) {
    const firstEmail = cont.emails.length === 0 ? '' : cont.emails[0].emailAddress;
    const firstPhone = cont.phones.length === 0 ? '' : cont.phones[0].phoneNumber;
    const emails = cont.emails.length === 0 ? cont.emails : cont.emails.splice(1, cont.emails.length - 1);
    this.temp_contact.email = firstEmail;
    this.temp_contact.phone = firstPhone;
    const phones = cont.phones.length === 0 ? cont.phones : cont.phones.splice(1, cont.phones.length - 1);
    const formEmails = this.formBuilder.array([]);
    const formPhones = this.formBuilder.array([]);

    emails.forEach((email: any) => {
        formEmails.controls.push(this.formBuilder.group({
          'name': [email.emailAddress, [Validators.required, ValidationService.emailValidator]],
        }));
      }
    )

    phones.forEach((phone: any) => {
      formPhones.controls.push(this.formBuilder.group({
        'name': [phone.phoneNumber, [Validators.required, ValidationService.numbersOnlyValidator]],
      }));
    })
    this.form = this.formBuilder.group({
      'firstname': [cont.firsttName, [Validators.required, ValidationService.lettersOnlyValidator]],
      'lastname': [cont.lastName, [Validators.required, ValidationService.lettersOnlyValidator]],
      'email': [firstEmail, [Validators.required, ValidationService.emailValidator]],
      'phone': [firstPhone, [Validators.required, ValidationService.numbersOnlyValidator]],
      'emails': formEmails,
      'phones': formPhones
    });
    delete this.temp_contact.phones;
    delete this.temp_contact.emails;
  }

  showContact(contact: any) {
    this.temp_contact = Object.assign({}, contact);
  }

  close() {
    this.search();
  }

  closeNew() {
    this.search();
  }

  DeleteElement(contactToDelete: any) {
    this.search();
  }

  search() {
    this.setPage(1);
  }

  changeFilterColumn() {
    const __this = this;
    this.filterValue = '';
    this.setPage(1);
  }

  setPage(page: number, sortOrder: string = '') {
    if (page < 1) {
      return;
    }
    this.contactsService.getPagedContacts(10, page, this.filterValue.toLocaleLowerCase(), this.filterColumn, sortOrder)
      .subscribe((contacts: any) => {
        this.pager = this.pagerService.getPager(contacts.total, page);
        this.contacts = contacts.contacts;
      });
  }


  sort(column: string, type: number) {
    this.setPage(1, column);
    this.sortNumber = type;
  }
}
