import { Component, OnInit, Input, AfterViewInit, ViewChildren } from '@angular/core';
import { DocumentFilter, DocumentCategory, Item } from '../../models/index.model';
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
    buttonIcon: string;
    queryValue: any;

    filterType: string;   
    itemList: Item[];

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
        this.setFilterType();

        let deleteDefaultFilter = new DocumentFilter("add", this, this.localFilterList[0], null);
        this.documentsService.chosenFilterEvent.next(deleteDefaultFilter);
    }

    ngAfterViewInit(): void {
        let self = this;

        let identifier = "#"+ this.dateIdentifier();
        $(identifier).datetimepicker({ useCurrent: false, format: "YYYY-MM-DD" });
        $(identifier).on("dp.change", function (e: any) {
            self.queryValue = $(identifier).val();
            self.onFilterValueChangeDetected();
        });
    }

    getSearchValue(): string {
        return this.chosenFilter;
    }

    onButtonClick(): void {
        if (this.buttonIcon == "fa-plus") {
            this.documentsService.newFilterEvent.next();
            this.buttonIcon = "fa-minus";

            let deleteSelected = new DocumentFilter("add", this, this.chosenFilter, null);
            this.documentsService.chosenFilterEvent.next(deleteSelected); 

            return;        
        }
        
        this.onFilterDeleted();
    }

    onFilterChanged(): void {
        this.setFilterType();

        let addPreviousSelected = new DocumentFilter("delete", this, this.previousChosenFilter, null);
        this.documentsService.chosenFilterEvent.next(addPreviousSelected);  
        this.onFilterValueChangeDetected();
        
        let deleteSelected = new DocumentFilter("add", this, this.chosenFilter, null);
        this.documentsService.chosenFilterEvent.next(deleteSelected);  
        
        this.previousChosenFilter = this.chosenFilter;        
    }

    onFilterDeleted(): void {
        let docFilModel = new DocumentFilter("delete", this, this.chosenFilter, null);
        this.documentsService.chosenFilterEvent.next(docFilModel);
        this._ref.destroy();        
    }

    onFilterChangeDetected(filterChange: DocumentFilter): void {
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

    onFilterValueChangeDetected(): void {
        let value = this.queryValue;

        if ( this.getFilterType(this.chosenFilter) == "date") {
            value = this.queryValue.split(" ")[0] + 'T' + "00:00:00.000" + 'Z';
        }
        else if ( this.getFilterType(this.chosenFilter) == "dropDown") {
            value = +value;
        }

        let documentFilter = new DocumentFilter(null, null, this.chosenFilter, value);
        this.documentsService.updateFilter.next(documentFilter);
    }

    setFilterType(): void {
        this.filterType = "input";
        this.itemList = null;

        if ( this.getFilterType(this.chosenFilter) == "date") {
            this.filterType = "date";
        }

        if ( this.getFilterType(this.chosenFilter) == "dropDown") {
            this.filterType = "dropDown";
            this.fillDropDown();
        }

        if ( this.getFilterType(this.chosenFilter) !== this.getFilterType(this.previousChosenFilter) ) {
            this.queryValue = '';
        }

        return;        
    }

    getFilterType(filter: string): string {
        if ( filter == "Title" || filter == "Description" ) {
            return "input";
        }
        
        if ( filter == "Case" || filter == "Category" ) {
            return "dropDown";
        }

        return "date";
    }

    fillDropDown(): void {
        if (this.chosenFilter == "Case") {
            this.documentsService.getCaseList()
                .subscribe( (cases: Item[]) => 
                {
                    this.itemList = cases;
                    this.queryValue = cases[0].id;
                    this.onFilterValueChangeDetected();                    
                });
        }

        else if (this.chosenFilter == "Category") {
            this.documentsService.getCategoryList()
                .subscribe( (categories: Item[]) => 
                {
                    this.itemList = categories;
                    this.queryValue = categories[0].id;   
                    this.onFilterValueChangeDetected();                    
                });
        }
    }

    dateIdentifier(): string {
        return "filterDate" + this.id;
    }
}
