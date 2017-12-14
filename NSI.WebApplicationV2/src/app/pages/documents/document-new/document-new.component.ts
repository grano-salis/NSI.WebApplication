import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-document-new',
    templateUrl: './document-new.component.html',
    styleUrls: ['./document-new.component.css']
})
export class DocumentNewComponent {
    @Input() scopedToCase: boolean;
}
