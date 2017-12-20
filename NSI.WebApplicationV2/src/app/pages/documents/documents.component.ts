import { Component, OnInit } from '@angular/core';
import { each } from 'lodash';
import * as moment from 'moment';
import { Logger } from '../../core/services/logger.service';
declare let $: any;

const logger = new Logger('documents');
@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.css']
})
export class DocumentsComponent implements OnInit {
  scopedToCase: boolean;

  constructor() { }

  ngOnInit() {
    this.scopedToCase = true;
  }


}
