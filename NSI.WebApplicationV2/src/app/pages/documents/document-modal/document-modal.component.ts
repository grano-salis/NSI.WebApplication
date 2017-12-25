import { Component, Input } from '@angular/core';

import { DocumentDetails } from '../models/documentDetails.model';

import { DocumentsService } from '../../../services/documents.service'
import { RequestOptions } from "@angular/http";
import {Headers} from '@angular/http';

@Component({
    selector: 'app-document-modal',
    templateUrl: './document-modal.component.html',
    styleUrls: ['./document-modal.component.css']
})
export class DocumentModalComponent { 
    @Input() scopedToCase: boolean;
    @Input() editMode: boolean;
    @Input() document: DocumentDetails;
    constructor(private documentsService : DocumentsService) {

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
    let fi = event.srcElement;
    if (fi.files && fi.files[0]) {
        let fileToUpload = fi.files[0];

        let formData:FormData = new FormData();
         formData.append(fileToUpload.name, fileToUpload);

        let headers = new Headers();
        headers.append('Accept', 'application/json');
        // DON'T SET THE Content-Type to multipart/form-data, You'll get the Missing content-type boundary error
        let options = new RequestOptions({ headers: headers });

        this.documentsService.uploadFile(formData, headers);
        }
    }
}
