import {Component, OnInit} from '@angular/core';
import {ContactsService} from '../../services/contacts.service';

import {PagerService} from "../../services/pagination.service";
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

  // pager object
  pager: any = {};

  // paged items
  pagedItems: any[];

  constructor(private contactsService: ContactsService, private pagerService: PagerService) {
    const _this = this;
    this.filterColumn = 'name';
    this.filterValue = '';
    this.temp_contact = new Contact();
  }

  ngOnInit() {
    const _this = this;
    this.contactsService.getContacts().subscribe((contacts: any) => {
      _this.allContacts = contacts;
      _this.setPage(1);
    });
  }

  addContact() {
    console.log('add contact');
  }

  editContact(contact: any) {
    this.temp_contact = Object.assign({}, contact);
    if (this.temp_contact.emails.length === 0) {
      this.temp_contact.emails.push('');
    }
    if (this.temp_contact.phones.length === 0) {
      this.temp_contact.phones.push('');
    }
  }

  newContact() {
    this.temp_contact = new Contact();
    this.temp_contact.emails = [];
    this.temp_contact.phones = [];
  }

  showContact(contact: any) {
    this.temp_contact = Object.assign({}, contact);
  }

  close() {
    this.allContacts[this.allContacts.findIndex((c: any) => c.contact1 === this.temp_contact.contact1)] = this.temp_contact;
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

  search() {
    const __this = this;
    const filterValue = this.filterValue.toLocaleLowerCase();
    this.contacts = this.contacts.filter((contact: any) => {
      if (this.filterColumn === 'name') {
        return contact.firsttName.toLocaleLowerCase().includes(filterValue) ||
          contact.lastName.toLocaleLowerCase().includes(filterValue) ||
          (contact.lastName + ' ' + contact.firsttName).toLocaleLowerCase().includes(filterValue) ||
          (contact.firsttName + ' ' + contact.lastName).toLocaleLowerCase().includes(filterValue);
      }
      if (this.filterColumn === 'phone') {
        for (const phone of contact.phones) {
          if (phone.phoneNumber.toLocaleLowerCase().includes(filterValue)) {
            return true;
          }
        }
        return false;
      }
      if (this.filterColumn === 'email') {
        for (const email of contact.emails) {
          if (email.emailAddress.toLocaleLowerCase().includes(filterValue)) {
            return true;
          }
        }
        return false;
      }
    });
    this.setPage(this.pager.currentPage);
  }

  changeFilterColumn() {
    const __this = this;
    this.filterValue = '';
    this.setPage(1);
  }

  setPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }
    this.pager = this.pagerService.getPager(this.allContacts.length, page);
    this.contactsService.getPagedContacts(this.pager.pageSize, page).subscribe((contacts: any) => {
      this.contacts = contacts;
    });
  }


  sort(type: number) {
    this.setPage(type);
  }
}
