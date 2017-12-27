import { Component, OnInit, Input } from '@angular/core';
import { each } from 'lodash';
import * as moment from 'moment';
import { Logger } from '../../core/services/logger.service';

const logger = new Logger('documents');
@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.css']
})
export class DocumentsComponent implements OnInit {
  @Input() scopedToCase: boolean;

  constructor() { }

  ngOnInit() {
    if (this.scopedToCase == null) {
      this.scopedToCase = false;
    }
  }

  toggleScopedToCase(setTo: string) {
    this.scopedToCase = (setTo == "toCase") ? true : ((setTo == "toDocument") ? false : this.scopedToCase);
  }
}
