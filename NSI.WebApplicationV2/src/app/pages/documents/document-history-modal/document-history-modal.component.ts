import { Component, Input, OnInit } from '@angular/core';

import { DocumentsService } from '../../../services/documents.service';
import { DocumentDetails } from '../models/documentDetails.model';

@Component({
  selector: 'app-document-history-modal',
  templateUrl: './document-history-modal.component.html',
  styleUrls: ['./document-history-modal.component.css']
})
export class DocumentHistoryModalComponent implements OnInit {
  @Input() scopedToCase: boolean;
  @Input() currentTitle: string;
  documents: DocumentDetails[];

  constructor(private documentsService: DocumentsService) { }

  ngOnInit() {
    this.documentsService.getDocuments().subscribe(
      (dT: DocumentDetails[]) => {
        this.documents = dT;
      } 
    );
}
}
