import { Component, OnInit, Input } from '@angular/core';
import { each } from 'lodash';
import * as moment from 'moment';

import { Logger } from '../../core/services/logger.service';
import { DocumentsService } from '../../services/documents.service';

const logger = new Logger('documents');

@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.css']
})
export class DocumentsComponent implements OnInit {
  @Input() scopedToCase: boolean;
  @Input() caseNumber: number;

  constructor(private documentsService: DocumentsService) { }

  ngOnInit() {
    if (this.scopedToCase == null) {
      this.scopedToCase = false;
    }
    else {
      this.documentsService.documentCaseChanged.next(this.caseNumber);    
    }
  }

  toggleScopedToCase(setTo: string) {
    this.scopedToCase = (setTo == "toCase") ? true : ((setTo == "toDocument") ? false : this.scopedToCase);
    this.documentsService.documentAll.next();
  }

  onCaseChanged() {
    this.documentsService.documentCaseChanged.next(this.caseNumber);        
  }
}
