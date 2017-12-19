import { Component, OnInit } from '@angular/core';
import { DocumentsService } from '../../../services/documents.service';

@Component({
    selector: 'app-document-filter',
    templateUrl: './document-filter.component.html',
    styleUrls: ['./document-filter.component.css']
})
export class DocumentFilterComponent implements OnInit {
    _ref: any;
    hasFollower: boolean;
    buttonSign: string;

    constructor(private documentsService: DocumentsService) { }

    ngOnInit() {
        this.buttonSign = '+';
    }

    onAddFilterComponent() {
        if (this.buttonSign == '+') {
          this.documentsService.newFilterEvent.next();
          this.buttonSign = '-';  
          return;        
        }
        this._ref.destroy();
        this.buttonSign = '+';
    }
}
