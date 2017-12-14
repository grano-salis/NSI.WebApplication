import { Component, OnInit, Input } from '@angular/core';
import { Document, DocumentDetails } from '../models/index.model';
import { DocumentsService } from '../../../services/documents.service';

@Component({
  selector: 'app-document-list',
  templateUrl: './document-list.component.html',
  styleUrls: ['./document-list.component.css']
})
export class DocumentListComponent implements OnInit { 
    @Input() scopedToCase: boolean;  
    documents: DocumentDetails[];

    constructor(private documentsService: DocumentsService) {}

    ngOnInit() {
        this.documentsService.getDocuments().subscribe(
          (dT: DocumentDetails[]) => {
            this.documents = dT;
          } 
        );
    }
}
