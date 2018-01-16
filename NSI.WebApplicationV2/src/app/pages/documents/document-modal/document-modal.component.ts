import { Component, Input, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';

import {Observable} from 'rxjs/Rx';

import { 
    Document, 
    DocumentDetails,
    Item
} from '../models/index.model';

import { DocumentsService } from '../../../services/documents.service'
import { RequestOptions, Headers } from '@angular/http';

@Component({
    selector: 'app-document-modal',
    templateUrl: './document-modal.component.html',
    styleUrls: ['./document-modal.component.css']
})
export class DocumentModalComponent { 
    @Input() scopedToCase: boolean;
    @Input() caseNumber: number;     
    @Input() editMode: boolean;

    @ViewChild('closeModal') closeModal: ElementRef;    

    caseList: Item[];
    categoryList: Item[];

    documentEditIndex: number;
    documentEditId: number;    
    docForm: FormGroup;
    document: Document;
    editingStarted: boolean;

    fileToUpload: any;
    fileToUploadTitle: string;
    uploadProgress: number;
    showFileUploadLabel: boolean;

    constructor(private documentsService : DocumentsService, private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.docForm = this.createGroup();
        this.document = new Document(null, null, null, null, null, null, null, null);
        this.editingStarted = false;
        this.fileToUploadTitle = "";
        this.showFileUploadLabel = true;
        this.uploadProgress = 0;        

        this.documentsService.documentUpdatingRequested.subscribe((value: DocumentDetails) => { this.editMode ? this.setFormValues(value) : '' });
        
        this.documentsService.getCaseList()
            .subscribe((list: Item[]) => { 
                this.caseList = list; 
                
                this.docForm.patchValue({
                    'CaseId': list[0].id,
                }); 
            });
        
        this.documentsService.getCategoryList()
            .subscribe((list: Item[]) => { 
                this.categoryList = list; 
                
                this.docForm.patchValue({
                    'CategoryId': list[0].id
                });
            });

        this.documentsService.documentUpdatingRequested.subscribe((value: DocumentDetails) => { this.editMode ? this.setFormValues(value) : '' });
    }

    createGroup(): FormGroup {
        return this.formBuilder.group({
            'Title':        [null, Validators.required],
            'Description':  [null],
            'CaseId':       [null, Validators.required],
            'CategoryId':   [null, Validators.required],
        });
    }

    onConfirmationAction(): void {
        this.editMode ? this.onUpdateCollection() : this.onAddToCollection();
    }

    onCancelAction(): void {
        this.onCanceled();
    }

    onAddToCollection(): void {
        if ( this.fileToUploadTitle == "" ) {
            this.addDocument();
            return;
        }

        let uploadFormData = new FormData();
        uploadFormData.append(this.fileToUpload.name, this.fileToUpload);
        
        this.documentsService.uploadFile(uploadFormData)
            .subscribe( (path: string) => 
                { 
                    this.document.documentPath = path;
                    this.addDocument();
                });

        if ( this.fileToUploadTitle != "" ) {
            this.showFileUploadLabel = false;
            Observable.interval(this.fileToUpload.size * 1.0 / 40000)
            .takeWhile( () => this.uploadProgress < 100)
            .subscribe( () => {
                this.uploadProgress += 10;    
                return this.uploadProgress;            
            });
        }
    }

    addDocument() {
        let formData = this.docForm.value;
        
        this.document.documentId = 0;
        this.document.documentTitle = formData.Title;
        this.document.documentDescription = formData.Description == null ? "" : formData.Description;
        this.document.caseId = formData.CaseId;
        this.document.categoryId = formData.CategoryId;
        this.document.documentContent = "";
        this.document.createdByUserId = 1;

        this.documentsService.postDocument(this.document)
            .subscribe( (responseDoc: DocumentDetails) => {
                this.documentsService.documentAdded.next(responseDoc);
                this.resetForm();
            });
    }

    onUpdateCollection(): void {
        if ( this.fileToUploadTitle == "" ) {
            this.editDocument();
            return;
        }

        let uploadFormData = new FormData();
        uploadFormData.append(this.fileToUpload.name, this.fileToUpload);
        
        this.documentsService.uploadFile(uploadFormData)
            .subscribe( (path: string) => { 
                this.document.documentPath = path;
                this.editDocument();
            });
    }

    editDocument() {
        let formData = this.docForm.value;
        
        let edit = new Document(this.document.documentId, formData.Title, formData.Description, formData.CaseId, 
            formData.CategoryId, this.document.documentContent, this.document.createdByUserId, this.document.documentPath);

        if ( this.fileToUploadTitle != "" ) {
            Observable.interval(this.fileToUpload.size * 1.0 / 40000)
            .takeWhile( () => this.uploadProgress < 100)
            .subscribe( () => {
                this.uploadProgress += 10;    
                return this.uploadProgress;            
            });
        }

        this.documentsService.putDocument(edit)
            .subscribe( (responseDoc: DocumentDetails) => {
                this.documentsService.documentUpdated.next(responseDoc);
                this.resetForm();
            });
    }

    onCanceled(): void {
        this.resetForm();
    }

    onFileChange(event: any): void {
        let fi = event.srcElement;
        if (fi.files && fi.files[0]) {
            this.fileToUpload = fi.files[0];
            this.fileToUploadTitle = fi.files[0].name;
        }
    }

    setFormValues(value: DocumentDetails): void {
        this.editingStarted = true;

        this.docForm.setValue({
            'Title': value.documentTitle,
            'Description': value.documentDescription,
            'CaseId': value.caseId,
            'CategoryId': value.categoryId
        });

        this.editingStarted = false;
        this.document = new DocumentDetails(value.documentId, value.documentTitle, value.documentDescription, value.caseId, 
            value.categoryId, value.documentContent, value.createdByUserId, value.fileTypeId, value.documentPath, value.author,
            value.caseNumber, value.documentCategoryName, value.fileIconPath, value.createdAt, value.modifiedAt);
    }

    resetForm(): void {
        this.docForm.reset();

        this.docForm.patchValue({
            'CaseId': this.caseList[0].id,
            'CategoryId': this.categoryList[0].id
        });

        this.fileToUpload = null;
        this.fileToUploadTitle = "";
        this.showFileUploadLabel = true;                
        this.uploadProgress = 0;
        this.closeModal.nativeElement.click();
    }

    modalId(): string {
        return this.editMode ? 'edit' : 'add';        
    }

    headerText(): string {
        return this.editMode ? 'MANAGE Document' : 'Add Document';
    }

    footerConfirmationText(): string {
        return this.editMode ? 'Save changes' : 'Add';
    }

    footerCancelText(): string {
        return this.editMode ? 'Cancel' : 'Cancel';
    }  
}
