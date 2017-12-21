import { Component, Input } from '@angular/core';

import { DocumentDetails } from '../models/documentDetails.model';

@Component({
    selector: 'app-document-modal',
    templateUrl: './document-modal.component.html',
    styleUrls: ['./document-modal.component.css']
})
export class DocumentModalComponent { 
    @Input() scopedToCase: boolean;
    @Input() editMode: boolean;
    @Input() document: DocumentDetails;

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
}
