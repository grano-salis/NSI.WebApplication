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
  sortNumber: number;
  pagedItems: any[];

  constructor(private contactsService: ContactsService, private pagerService: PagerService) {
    const _this = this;
    this.filterColumn = 'name';
    this.filterValue = '';
    this.temp_contact = new Contact();
    this.sortNumber = 0;
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
