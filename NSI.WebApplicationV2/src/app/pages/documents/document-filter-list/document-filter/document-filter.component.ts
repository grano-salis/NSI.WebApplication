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
    queryValue: any;

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
        this.setIsDateFilter();

        let deleteDefaultFilter = new DocumentFilter("add", this, this.localFilterList[0], null);
        this.documentsService.chosenFilterEvent.next(deleteDefaultFilter);
    }

    ngAfterViewInit(): void {
        let self = this;

        let identifier = "#"+ this.dateIdentifier();
        $(identifier).datetimepicker({ useCurrent: false, format: "YYYY-MM-DD HH:mm:ss.SSS" });
        $(identifier).on("dp.change", function (e: any) {
            self.queryValue = $(identifier).val();
            self.onFilterValueChangeDetected();
        });
    }

    getSearchValue() {
        return this.chosenFilter;
    }

    onButtonClick() {
        if (this.buttonIcon == "fa-plus") {
            this.documentsService.newFilterEvent.next();
            this.buttonIcon = "fa-minus";

            let deleteSelected = new DocumentFilter("add", this, this.chosenFilter, null);
            this.documentsService.chosenFilterEvent.next(deleteSelected); 

            return;        
        }
        
        this.onFilterDeleted();
    }

    onFilterChanged() {
        this.setIsDateFilter();

        let addPreviousSelected = new DocumentFilter("delete", this, this.previousChosenFilter, null);
        this.documentsService.chosenFilterEvent.next(addPreviousSelected);  
        this.onFilterValueChangeDetected();
        
        let deleteSelected = new DocumentFilter("add", this, this.chosenFilter, null);
        this.documentsService.chosenFilterEvent.next(deleteSelected);  
        
        this.previousChosenFilter = this.chosenFilter;        
    }

    onFilterDeleted() {
        let docFilModel = new DocumentFilter("delete", this, this.chosenFilter, null);
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
                if (this.localFilterList[i] === filterChange.field) {
                    this.localFilterList.splice(i, 1);
                    break;
                }
            }
        }
        else if (filterChange.type == "delete")
        {
            this.localFilterList = this.documentsService.pushFilterSorted(filterChange.field, this.localFilterList);
        }
    }

    onFilterValueChangeDetected() {
        let value = this.queryValue;

        if ( this.checkIfDateFilter(this.chosenFilter) ) {
            let explode = this.queryValue.split(" ");
            value = explode[0] + 'T' + explode[1] + 'Z';
        }

        let documentFilter = new DocumentFilter(null, null, this.chosenFilter, value);
        this.documentsService.updateFilter.next(documentFilter);
    }

    setIsDateFilter(): void {
        this.isDateFilter = false;

        if ( this.checkIfDateFilter(this.chosenFilter) ) {
            this.isDateFilter = true;
        }

        if ( this.checkIfDateFilter(this.chosenFilter) !== this.checkIfDateFilter(this.previousChosenFilter) ) {
            this.queryValue = '';
        }

        return;        
    }

    checkIfDateFilter(filter: string): boolean {
        if ( filter == "Title" || filter == "Description" || filter == "Case" || filter == "Category" ) {
            return false;
        }

        return true;
    }

    dateIdentifier(): string {
        return "filterDate" + this.id;
    }
}
