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
    selectedDocumentTitle: string;
    selectedDocument: DocumentDetails;
    toBeEditedItem: Document;
    toBeDeletedItemIndex: number;

    constructor(private documentsService: DocumentsService) {}

    ngOnInit() {
        this.documentsService.getDocuments().subscribe(
          (dT: DocumentDetails[]) => {
            this.documents = dT;
          } 
        );
    }

    onPreEdit(item: DocumentDetails): void {
        this.toBeEditedItem.documentId = item.documentId;
    }

    onPreDelete(index: number): void {
        this.toBeDeletedItemIndex = index;
    }

    onDeleteItem(): void {
        this.documents.splice(this.toBeDeletedItemIndex, 1);
    }

    onCancelDelete(): void {
        this.toBeDeletedItemIndex = -1;
    }
}
