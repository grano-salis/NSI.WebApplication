import {AfterViewInit, Component, OnInit} from '@angular/core';
import {each} from 'lodash';
import * as moment from 'moment';
import {Logger} from '../../core/services/logger.service';
import {ContactsService} from "../../services/contacts.service";
declare let $: any;

const logger = new Logger('contacts');
@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: []
})
export class ContactsComponent implements OnInit, AfterViewInit {
  contacts: any[];

  constructor(private contactsService: ContactsService) {
    this.contacts = [{
      'FirstName': 'Admira',
      'LastName': 'Husic',
      'Email': 'admira@nsi.com',
      'Phone': '1'
    },
      {
        'FirstName': 'Vejsil',
        'LastName': 'Hrustic',
        'Email': 'vejs@nsi.com',
        'Phone': '2'
      },
      {
        'FirstName': 'Aida',
        'LastName': 'Kanlic',
        'Email': 'aida@nsi.com',
        'Phone': '3'
      },
      {
        'FirstName': 'Merima',
        'LastName': 'Cisija',
        'Email': 'merima@nsi.com',
        'Phone': '4'
      },
      {
        'FirstName': 'Adna',
        'LastName': 'Tahic',
        'Email': 'adna@nsi.com',
        'Phone': '5'
      }];
  }


  ngOnInit() {
    this.contactsService.getContacts().subscribe((contacts: any) => {
    });
  }

  ngAfterViewInit() {
    $('#datatable').dataTable();
  }

}
