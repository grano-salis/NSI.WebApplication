import {Component, OnInit} from '@angular/core';
import {ContactsService} from '../../services/contacts.service';
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
  styleUrls: []
})

export class ContactsComponent implements OnInit {
  contacts: any[];
  temp_contact: any;


  constructor(private contactsService: ContactsService) {
    const _this = this;
    setTimeout(function () {
      $(function () {
        _this.initTable();
      });
    }, 700);
    this.temp_contact = new Contact();
  }

  ngOnInit() {
    const _this = this;
    this.contactsService.getContacts().subscribe((contacts: any) => {
      _this.contacts = contacts;
    });
  }

  addContact() {
    console.log('add contact');
  }

  editContact(contact: any) {
    this.temp_contact = Object.assign({}, contact);
  }

  newContact() {
    this.temp_contact = new Contact();
  }

  showContact(contact: any) {
    this.temp_contact = Object.assign({}, contact);
  }

  close() {
    const _this = this;
    this.contacts[this.contacts.findIndex((c: any) => c.contact1 === this.temp_contact.contact1)] = this.temp_contact;
    $('#datatable').dataTable().fnDestroy();
    setTimeout(function () {
      $(function () {
        _this.initTable();
      });
    }, 100);
  }

  closeNew() {
    const _this = this;
    this.contacts.push(this.temp_contact);
    $('#datatable').dataTable().fnDestroy();
    setTimeout(function () {
      $(function () {
        _this.initTable();
      });
    }, 100);

  }

  DeleteElement(contactToDelete: any) {
    const _this = this;
    const index = this.contacts.findIndex((c: any) => c.contact1 === contactToDelete.contact1)
    this.contacts.splice(index, 1);
    $('#datatable').dataTable().fnDestroy();
    setTimeout(function () {
      $(function () {
        _this.initTable();
      });
    }, 100);
  }

  initTable() {
    $('#datatable').dataTable(
      {
        'bAutoWidth': false,
        'aoColumns': [
          {'bSortable': true},
          {'bSortable': true},
          {'bSortable': true},
          {'bSortable': false},
          {'bSortable': false},
          {'bSortable': false}
        ]
      }
    );
  }
}
