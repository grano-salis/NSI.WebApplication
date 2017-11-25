import { Component, OnInit } from '@angular/core';
import { each } from 'lodash';
import * as moment from 'moment';
import { Logger } from '../../core/services/logger.service';
declare let $: any;

const logger = new Logger('contacts');
@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: []
})
export class ContactsComponent implements OnInit {


  constructor() {

  }

  ngOnInit() {
  }


}
