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
    toBeDeletedItemIndex: number;
    toBeDeletedItemId: number;
    
    constructor(private documentsService: DocumentsService) {}

    ngOnInit() {
        this.documentsService.getDocuments().subscribe(
          (dT: DocumentDetails[]) => {
            this.documents = dT;
          } 
        );
    }

    onPreEdit(value: DocumentDetails): void {
        let document = new DocumentDetails(value.documentId, value.documentTitle, value.documentDescription, value.caseId, 
            value.categoryId, value.documentContent, value.createdByUserId, value.fileTypeId, value.documentPath, value.author,
            value.caseNumber, value.documentCategoryName, value.fileIconPath, value.createdAt, value.modifiedAt);

        this.documentsService.documentUpdatingRequested.next(document);
    }

    onPreDelete(index: number, id: number): void {
        this.toBeDeletedItemIndex = index;
        this.toBeDeletedItemId = id;
    }

    onDeleteItem(): void {
        this.documentsService.deleteDocument(this.toBeDeletedItemId)
            .subscribe( () => this.documents.splice(this.toBeDeletedItemIndex, 1));
    }

    onCancelDelete(): void {
        this.toBeDeletedItemIndex = -1;
    }

    subscribe(): void {
    //     this.addressService.addressAdded.subscribe((address: AddressDetail) => { this.addresses.push(address); });
    //     this.addressService.addressUpdated.subscribe((item: { index: number, address: AddressDetail }) => { this.addresses[item.index] = item.address });
    // 
    }
}
