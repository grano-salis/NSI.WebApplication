import { Component, OnInit, Input } from '@angular/core';
import { Document, DocumentDetails, DocumentQuery } from '../models/index.model';
import { DocumentsService } from '../../../services/documents.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { DocumentFilter } from '../models/documentFilter.model';

@Component({
  selector: 'app-document-list',
  templateUrl: './document-list.component.html',
  styleUrls: ['./document-list.component.css']
})
export class DocumentListComponent implements OnInit { 
    @Input() scopedToCase: boolean; 
    @Input() caseNumber: number;

    documents: DocumentDetails[];

    selectedDocument: DocumentDetails;
    selectedDocumentTitle: string;

    toBeUpdatedItemIndex: number;
    toBeDeletedItemIndex: number;
    toBeDeletedItemId: number;

    queryModel: DocumentQuery;
    numberOfVisiblePages: number;
    totalItems: number;
    itemsPerPage: number;
    currPage: number;
    pages: number[];
    
    constructor(private documentsService: DocumentsService, private sanitizer: DomSanitizer) { }

    ngOnInit() {
        this.queryModel = new DocumentQuery(1, 5);
        this.numberOfVisiblePages = 5;
        this.currPage = 1;

        this.subscribe();
        this.updatePage();     
    }

    onPreEdit(index: number, value: DocumentDetails): void {
        this.toBeUpdatedItemIndex = index;

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
        if ( this.currPage == Math.ceil(this.totalItems / this.itemsPerPage) ) {
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
                let numberOfPages = Math.ceil(this.totalItems * 1.0 / this.itemsPerPage);
                
                let offset1 = numberOfPages >= this.numberOfVisiblePages ? numberOfPages - (this.numberOfVisiblePages - 1) : 1;
                let offset2 = this.currPage > Math.floor(this.numberOfVisiblePages / 2) ? this.currPage - Math.floor(this.numberOfVisiblePages / 2) : 1
                let offset = Math.min(offset1, offset2);

                this.pages = Array.apply(null, { length: Math.min(this.numberOfVisiblePages, numberOfPages) })
                                .map(function(element: any, index: any) { 
                                    return index + offset; 
                                });
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
        
        this.documentsService.chosenFilterEvent
            .subscribe( (filter: DocumentFilter) =>
                {
                    if ( filter.type == "delete" ) {
                        filter.value = null;
                        this.setFilter(filter);
                    }
                }); 

        this.documentsService.updateFilter
            .subscribe( (filter: DocumentFilter) =>
                {
                    this.setFilter(filter);
                }); 

        this.documentsService.submitFiltering
            .subscribe( () =>
                {
                    this.updatePage();
                }); 

        this.documentsService.documentAdded
            .subscribe((document: DocumentDetails) => 
                { 
                    console.log("Add: " + JSON.stringify(document));
                    this.documents.unshift(document); 
                });
        
        this.documentsService.documentUpdated
            .subscribe((document: DocumentDetails) => 
                {          
                    // console.log("Indeks: " + this.toBeUpdatedItemIndex);                    
                    // console.log("Prije: " + JSON.stringify(this.documents[this.toBeUpdatedItemIndex]));
                    // console.log("Poslije: " + JSON.stringify(document));
                    this.documents[this.toBeUpdatedItemIndex] = document; 
                });
    }

    setFilter(filter: DocumentFilter) {
        switch(filter.field) {
            case "Title":
                this.queryModel.searchByTitle = filter.value;
                break;
            case "Case":
                this.queryModel.searchByCaseId = filter.value;
                break;
            case "Category":
                this.queryModel.searchByCategoryId = filter.value;
                break;
            case "Description":
                this.queryModel.searchByDescription = filter.value;
                break;
            case "CreatedBefore":
                this.queryModel.createdDateTo = filter.value;
                break;
            case "CreatedAfter":
                this.queryModel.createdDateFrom = filter.value;
                break;
            case "ModifiedBefore":
                this.queryModel.modifiedDateTo = filter.value;
                break;
            case "ModifiedAfter":
                this.queryModel.modifiedDateFrom = filter.value;
                break;
        }
    }

    shouldDisplayPrev(): boolean {
        if (this.currPage == 1) {
            return false;
        }
        return true;
    }

    shouldDisplayNext(): boolean {
        if (this.currPage == Math.ceil(this.totalItems / this.itemsPerPage)) {
            return false;
        }
        return true;
    }

    sanitize(url: string): SafeUrl {
        return this.sanitizer.bypassSecurityTrustUrl(url);
    }
}
