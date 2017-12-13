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
  styleUrls: ['./contacts.component.css']
})

export class ContactsComponent implements OnInit {
  contacts: any[];
  allContacts: any[];
  temp_contact: any;
  filterColumn: string;
  filterValue: string;

  constructor(private contactsService: ContactsService) {
    const _this = this;
    this.filterColumn = 'name';
    this.filterValue = '';
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
      _this.allContacts = contacts;
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
    this.temp_contact.emails =[];
    this.temp_contact.phones = [];
  }

  showContact(contact: any) {
    this.temp_contact = Object.assign({}, contact);
  }

  close() {
    this.allContacts[this.allContacts.findIndex((c: any) => c.contact1 === this.temp_contact.contact1)] =
      this.temp_contact;
    this.search();
  }

  closeNew() {
    this.allContacts.push(this.temp_contact);
    this.search();
  }

  DeleteElement(contactToDelete: any) {
    const index = this.allContacts.findIndex((c: any) => c.contact1 === contactToDelete.contact1);
    this.allContacts.splice(index, 1);
    this.search();
  }

  initTable() {
    $('#datatable').dataTable(
      {
        'searching': false,
        'bAutoWidth': false,
        'bLengthChange': false,
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

  search() {
    const __this = this;
    const filterValue = this.filterValue.toLocaleLowerCase();
    this.contacts = this.allContacts.filter((contact: any) => {
      if (this.filterColumn === 'name') {
        return contact.firsttName.toLocaleLowerCase().includes(filterValue) ||
          contact.lastName.toLocaleLowerCase().includes(filterValue) ||
          (contact.lastName + ' ' + contact.firsttName).toLocaleLowerCase().includes(filterValue) ||
          (contact.firsttName + ' ' + contact.lastName).toLocaleLowerCase().includes(filterValue);
      }
      return contact[this.filterColumn].toLocaleLowerCase().includes(filterValue);
    });
    $('#datatable').dataTable().fnDestroy();
    setTimeout(function () {
      $(function () {
        __this.initTable();
      });
    }, 100);
  }

  changeFilterColumn() {
    const __this = this;
    this.filterValue = '';
    this.contacts = this.allContacts;
    $('#datatable').dataTable().fnDestroy();
    setTimeout(function () {
      $(function () {
        __this.initTable();
      });
    }, 100);
  }
}
