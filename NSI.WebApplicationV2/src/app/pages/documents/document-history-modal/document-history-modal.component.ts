import { Component, Input, OnInit } from '@angular/core';

import { DocumentsService } from '../../../services/documents.service';
import { DocumentDetails, DocumentHistory } from '../models/index.model';

@Component({
  selector: 'app-document-history-modal',
  templateUrl: './document-history-modal.component.html',
  styleUrls: ['./document-history-modal.component.css']
})
export class DocumentHistoryModalComponent implements OnInit {
  @Input() scopedToCase: boolean;
  @Input() currentTitle: string;
  documentDetails: DocumentDetails;
  documentHistories: DocumentHistory[];

  constructor(private documentsService: DocumentsService) { }

  ngOnInit() {
    this.subscribe();
  }

  subscribe() {
    this.documentsService.documentHistoryRequested
      .subscribe( (documentDetails: DocumentDetails) => 
      {
        this.documentDetails = documentDetails;
        this.documentsService.getDocumentHistoryByDocumentId(documentDetails.documentId).subscribe(
          (dH: DocumentHistory[]) => {
            this.documentHistories = dH;
          } 
        );
      });
  }
}
