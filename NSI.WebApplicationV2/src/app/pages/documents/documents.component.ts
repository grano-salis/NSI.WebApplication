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
  
  caseNumberValid: boolean;

  constructor(private documentsService: DocumentsService) { }

  ngOnInit() {
    if (this.scopedToCase == null) {
      this.scopedToCase = false;
    }
    else {
      this.documentsService.documentCaseChanged.next(this.caseNumber);    
    }

    this.caseNumberValid = true;
  }

  toggleScopedToCase(setTo: string) {
    this.scopedToCase = (setTo == "toCase") ? true : ((setTo == "toDocument") ? false : this.scopedToCase);
    
    if ( this.scopedToCase == false ) {
      this.documentsService.documentAll.next();
      this.caseNumber = null;
    }
  }

  onCaseChanged() {
    if ( +this.caseNumber ) {
      this.documentsService.documentCaseChanged.next(this.caseNumber);    
      this.caseNumberValid = true; 
    }   
    else {
      this.caseNumberValid = false;
    }
  }
}
