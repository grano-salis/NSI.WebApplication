import { Component, OnInit, Input, AfterViewInit, ViewChildren } from '@angular/core';
import { DocumentFilter, DocumentQuery } from '../../models/index.model';
import { DocumentsService } from '../../../../services/documents.service';

declare var $: any;

@Component({
    selector: 'app-document-filter',
    templateUrl: './document-filter.component.html',
    styleUrls: ['./document-filter.component.css']
})
export class DocumentFilterComponent implements OnInit, AfterViewInit {
    localFilterList: string[];
    scopedToCase: boolean = false;

    _ref: any;
    id: number;
    previousChosenFilter: string;
    chosenFilter: string
    hasFollower: boolean;
    isDateFilter: boolean;
    buttonIcon: string;
    queryModel: DocumentQuery;

    constructor(private documentsService: DocumentsService) { }

    ngOnInit() {
        this.queryModel = new DocumentQuery(0, 0);
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
        this.setIsDateFilter();

        let deleteDefaultFilter = new DocumentFilter("add", this, this.localFilterList[0]);
        this.documentsService.chosenFilterEvent.next(deleteDefaultFilter);
    }

    ngAfterViewInit(): void {
        $("#"+ this.dateIdentifier()).datetimepicker({ useCurrent: false, format: "MM/DD/YYYY, hh:mm:ss" });
    }

    getSearchValue() {
        return this.chosenFilter;
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
        this.setIsDateFilter();

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

    setIsDateFilter(): void {
        this.isDateFilter = true;

        if ( this.checkIfDateFilter(this.chosenFilter) ) {
            this.isDateFilter = false;
        }

        if ( this.checkIfDateFilter(this.chosenFilter) !== this.checkIfDateFilter(this.previousChosenFilter) ) {
            this.queryModel.searchByTitle = '';
        }

        return;        
    }

    checkIfDateFilter(filter: string): boolean {
        if ( filter == "Title" || filter == "Description" || filter == "Case" || filter == "Category" ) {
            return true;
        }

        return false;
    }

    dateIdentifier(): string {
        return "filterDate" + this.id;
    }
}
