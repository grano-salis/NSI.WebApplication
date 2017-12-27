import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';

import { 
    Document, 
    DocumentDetails,
    ListItem
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
    @Input() editMode: boolean;

    caseList: ListItem[];
    categoryList: ListItem[];

    documentEditIndex: number;
    documentEditId: number;    
    docForm: FormGroup;
    document: Document;
    editingStarted: boolean;

    constructor(private documentsService : DocumentsService, private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.docForm = this.createGroup();
        this.document = new Document(null, null, null, null, null, null, null, null);
        this.editingStarted = false;

        this.documentsService.documentUpdatingRequested.subscribe((value: DocumentDetails) => { this.editMode ? this.setFormValues(value) : '' });
        
        this.caseList = [new ListItem(1, "12345678"), new ListItem(2, "72381231"), new ListItem(3, "43257465")];
        this.categoryList = [new ListItem(1, "Invoice"), new ListItem(2, "Evidence"), new ListItem(3, "Category3")];
        //this.documentsService.getCaseList().subscribe((list: number[]) => { this.caseList = list; });
        //this.documentsService.getCategoryList().subscribe((list: number[]) => { this.categoryList = list; });

        this.docForm.patchValue({
            'CaseId': this.caseList[0].id,
            'CategoryId': this.categoryList[0].id
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
        let formData = this.docForm.value;
        // var currentdate = new Date(); 
        // var datetime =  currentdate.getDate() + "/"
        //                 + (currentdate.getMonth()+1)  + "/" 
        //                 + currentdate.getFullYear() + " "  
        //                 + currentdate.getHours() + ":"  
        //                 + currentdate.getMinutes() + ":" 
        //                 + currentdate.getSeconds();

        this.document.documentTitle = formData.Title;
        this.document.documentDescription = formData.Description == null ? "" : formData.Description;
        this.document.caseId = formData.caseId;
        this.document.categoryId = formData.categoryId;
        this.document.documentContent = "";
        this.document.createdByUserId = 1;

        this.documentsService.postDocument(this.document).subscribe(() => {
            this.resetForm();
        });
    }

    onUpdateCollection(): void {
        let formData = this.docForm.value;
        let edit = new Document(this.document.documentId, formData.Title, formData.Description, formData.CaseId, 
            formData.CategoryId, this.document.documentContent, this.document.createdByUserId, this.document.documentPath);
        this.documentsService.putDocument(this.documentEditIndex, edit)
            .subscribe(() => {
                this.resetForm();
            });
    }

    onCanceled(): void {
        //this.resetForm();
    }

    setFormValues(value: DocumentDetails) {
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
        //this.closeModal.nativeElement.click();
    }

    modalId() {
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

    onFileChange(event: any) {
        let fileList: FileList = event.target.files;
        if(fileList.length > 0) {
            let file: File = fileList[0];
            let formData:FormData = new FormData();
            formData.append('uploadFile', file, file.name);
            let headers = new Headers();
            /** No need to include Content-Type in Angular 4 */
            headers.append('Content-Type', 'multipart/form-data');
            headers.append('Accept', 'application/json');
            let options = new RequestOptions({ headers: headers });
            this.documentsService.uploadFile(formData, options);
        }
    }

    // onFileChange(event: any) {
    //     let fi = event.srcElement;
    //     if (fi.files && fi.files[0]) {
    //         let fileToUpload = fi.files[0];

    //         let formData:FormData = new FormData();
    //         formData.append(fileToUpload.name, fileToUpload);

    //         let headers = new Headers();
    //         headers.append('Accept', 'application/json');
    //         // DON'T SET THE Content-Type to multipart/form-data, You'll get the Missing content-type boundary error
    //         let options = new RequestOptions({ headers: headers });

    //         this.documentsService.uploadFile(formData, headers);
    //     }
    // }
}
