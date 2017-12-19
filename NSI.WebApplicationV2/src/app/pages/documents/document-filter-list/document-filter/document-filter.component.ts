import { Component, OnInit } from '@angular/core';
import { DocumentsService } from '../../../../services/documents.service';

@Component({
    selector: 'app-document-filter',
    templateUrl: './document-filter.component.html',
    styleUrls: ['./document-filter.component.css']
})
export class DocumentFilterComponent implements OnInit {
    _ref: any;
    hasFollower: boolean;
    buttonIcon: string;

    constructor(private documentsService: DocumentsService) { }

    ngOnInit() {
        this.buttonIcon = 'fa-plus';
    }

    onAddFilterComponent() {
        if (this.buttonIcon == "fa-plus") {
          this.documentsService.newFilterEvent.next();
          this.buttonIcon = "fa-minus";  
          return;        
        }
        this._ref.destroy();
        this.buttonIcon = "fa-plus";
    }
}
