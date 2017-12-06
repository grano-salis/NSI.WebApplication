import {AfterViewInit, Component, OnInit} from '@angular/core';
import {each} from 'lodash';
import * as moment from 'moment';
import {Logger} from '../../core/services/logger.service';
import {ContactsService} from '../../services/contacts.service';
const logger = new Logger('contacts');
declare var $: any;
@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: []
})
export class ContactsComponent implements OnInit {
  contacts: any[];

  constructor(private contactsService: ContactsService) {
    setTimeout(function () {
      $(function () {
        $('#datatable').dataTable();
      });
    }, 400);

  }

  ngOnInit() {
    const _this = this;
    this.contactsService.getContacts().subscribe((contacts: any) => {
      _this.contacts = contacts;
    });
  }

  DeleteElement(contactToDelete: any) {
    const index = this.contacts.indexOf(contactToDelete);
    this.contacts.splice(index, 1);
  }

}
