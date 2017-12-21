import { Component, OnInit, Input } from '@angular/core';
import { DocumentFilter } from '../../models/documentFilter.model';
import { DocumentsService } from '../../../../services/documents.service';

@Component({
    selector: 'app-document-filter',
    templateUrl: './document-filter.component.html',
    styleUrls: ['./document-filter.component.css']
})
export class DocumentFilterComponent implements OnInit {
    localFilterList: string[];
    scopedToCase: boolean = false;

    _ref: any;
    previousChosenFilter: string;
    chosenFilter: string
    hasFollower: boolean;
    buttonIcon: string;

    constructor(private documentsService: DocumentsService) { }

    ngOnInit() {
        this.buttonIcon = 'fa-plus';

        this.documentsService.chosenFilterEvent
            .subscribe((filterChange: DocumentFilter) => this.onFilterChangeDetected(filterChange));
        
        this.localFilterList = Object.assign([], this.documentsService.filterList);
        if (this.scopedToCase) {
            let casePosition = this.localFilterList.indexOf("Case");
            this.localFilterList.splice(casePosition, 1);
        }

        this.previousChosenFilter = this.localFilterList[0];
        this.chosenFilter = this.localFilterList[0];

        let deleteDefaultFilter = new DocumentFilter("add", this, this.localFilterList[0]);
        this.documentsService.chosenFilterEvent.next(deleteDefaultFilter);
    }

    onButtonClick() {
        if (this.buttonIcon == "fa-plus") {
            this.documentsService.newFilterEvent.next();
            this.buttonIcon = "fa-minus";

            let deleteSelected = new DocumentFilter("add", this, this.chosenFilter);
            this.documentsService.chosenFilterEvent.next(deleteSelected); 

            return;        
        }
        
        this.onFilterDeleted();
    }

    onFilterChanged() {
        let addPreviousSelected = new DocumentFilter("delete", this, this.previousChosenFilter);
        this.documentsService.chosenFilterEvent.next(addPreviousSelected);  
        
        let deleteSelected = new DocumentFilter("add", this, this.chosenFilter);
        this.documentsService.chosenFilterEvent.next(deleteSelected);  
        
        this.previousChosenFilter = this.chosenFilter;        
    }

    onFilterDeleted() {
        let docFilModel = new DocumentFilter("delete", this, this.chosenFilter);
        this.documentsService.chosenFilterEvent.next(docFilModel);
        this._ref.destroy();        
    }

    onFilterChangeDetected(filterChange: DocumentFilter) {
        if (filterChange.component === this) 
        {
            return;
        }

        if (filterChange.type == "add") 
        {
            for (let i = this.localFilterList.length - 1; i >= 0; i--) {
                if (this.localFilterList[i] === filterChange.value) {
                    this.localFilterList.splice(i, 1);
                    break;
                }
            }
        }
        else if (filterChange.type == "delete")
        {
            this.localFilterList = this.documentsService.pushFilterSorted(filterChange.value, this.localFilterList);
        }
    }
}
