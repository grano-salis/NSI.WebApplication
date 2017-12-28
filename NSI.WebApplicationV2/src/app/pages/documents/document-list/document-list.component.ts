import { Component, OnInit, Input } from '@angular/core';
import { Document, DocumentDetails, DocumentQuery } from '../models/index.model';
import { DocumentsService } from '../../../services/documents.service';

@Component({
  selector: 'app-document-list',
  templateUrl: './document-list.component.html',
  styleUrls: ['./document-list.component.css']
})
export class DocumentListComponent implements OnInit { 
    @Input() scopedToCase: boolean; 
    @Input() caseNumber: number; 
    documents: DocumentDetails[];

    selectedDocumentTitle: string;
    selectedDocument: DocumentDetails;

    toBeDeletedItemIndex: number;
    toBeDeletedItemId: number;

    queryModel: DocumentQuery;
    totalItems: number = 6;
    itemsPerPage: number = 3;
    currPage: number = 1;
    pages: number[];
    
    constructor(private documentsService: DocumentsService) {}

    ngOnInit() {
        this.queryModel = new DocumentQuery(1, 5);
        this.subscribe();
        this.updatePage();     
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
            .subscribe( () => {
                this.documents.splice(this.toBeDeletedItemIndex, 1);
                this.selectPage(this.currPage);              
            });
    }

    onCancelDelete(): void {
        this.toBeDeletedItemIndex = -1;
    }

    onEntriesChanged() {
        this.selectPage(1);
    }

    onOpenDocumentHistory(documentTitle: string, documentDetails: DocumentDetails): void {
        this.selectedDocumentTitle = documentTitle;
        this.documentsService.documentHistoryRequested.next(documentDetails);
    }
 
    prevPage(): void {
        if ( this.currPage == 1 ) {
            return;
        }

        this.currPage -= 1;
        this.queryModel.pageNumber = this.currPage;

        this.updatePage();
    }

    selectPage(page: number): void {
        this.currPage = page;
        this.queryModel.pageNumber = page;

        this.updatePage();
    }

    nextPage(): void {
        if ( this.currPage == (this.totalItems / this.itemsPerPage) ) {
            return;
        }

        this.currPage += 1;
        this.queryModel.pageNumber = this.currPage;        

        this.updatePage();
    }

    updatePage(): void {
        this.documentsService.getDocumentsWithPaging(this.queryModel).subscribe(
            (page: any) => {
              this.totalItems = page.totalItems;
              this.itemsPerPage = page.itemsPerPage;
              
              this.documents = page.results;
              this.pages = Array.apply(null, { length: Math.ceil(this.totalItems * 1.0 / this.itemsPerPage) }).map(function(element: any, index: any) { return index + 1; });
            } 
        );
    }

    subscribe(): void {
        this.documentsService.documentCaseChanged
            .subscribe((caseNo: number) => 
                { 
                    this.queryModel.searchByCaseId = caseNo;
                    this.updatePage();    
                });
        
        this.documentsService.documentAll
            .subscribe( () =>
                {
                    this.queryModel.searchByCaseId = 0;
                    this.updatePage();
                });

    //     this.addressService.addressAdded.subscribe((address: AddressDetail) => { this.addresses.push(address); });
    //     this.addressService.addressUpdated.subscribe((item: { index: number, address: AddressDetail }) => { this.addresses[item.index] = item.address });
    // 
    }
}
