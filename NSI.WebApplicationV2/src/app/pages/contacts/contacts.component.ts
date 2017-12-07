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
    setTimeout(function () {
      $(function () {
        $('#datatable').dataTable(
          {   'bAutoWidth': false,
          'aoColumns': [
          { 'bSortable': true },
          { 'bSortable': true },
          { 'bSortable': true },
          { 'bSortable': false },
          { 'bSortable': false },
          { 'bSortable': false }
        ]}
        );
      });
    }, 1000);
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

  close() {
    this.contacts[this.contacts.findIndex((c: any) => c.contact1 === this.temp_contact.contact1)] = this.temp_contact;
  }

  DeleteElement(contactToDelete: any) {
    const index = this.contacts.indexOf(contactToDelete);
    this.contacts.splice(index, 1);
  }

}
