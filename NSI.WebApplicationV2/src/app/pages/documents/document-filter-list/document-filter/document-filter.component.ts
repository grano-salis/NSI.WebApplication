import { Component, OnInit, Input, AfterViewInit, ViewChildren } from '@angular/core';
import { DocumentFilter, DocumentCase, DocumentCategory, DropDown } from '../../models/index.model';
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
    dropDownArray: DropDown[];

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
        this.setFilterType();

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

        if ( this.getFilterType(this.chosenFilter) == "date") {
            value = this.queryValue.split(" ")[0] + 'T' + "00:00:00.000" + 'Z';
        }

        let documentFilter = new DocumentFilter(null, null, this.chosenFilter, value);
        this.documentsService.updateFilter.next(documentFilter);
    }

    setFilterType(): void {
        this.filterType = "input";
        this.dropDownArray = null;

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

    fillDropDown() {
        if (this.filterType == "Case") {
            this.documentsService.getCaseList()
                .subscribe( (cases: DocumentCase[]) => 
                {
                    for (let index in cases) {
                        this.dropDownArray.push(this.mapToDDFromCase(cases[index]));
                    }
                });
        }

        else if (this.filterType == "Category") {
            this.documentsService.getCategoryList()
            .subscribe( (categories: DocumentCategory[]) => 
            {
                for (let index in categories) {
                    this.dropDownArray.push(this.mapToDDFromCategory(categories[index]));
                }
            });
        }
    }

    mapToDDFromCase(docCase: DocumentCase) {
        return new DropDown(
            docCase.caseId,
            docCase.caseNumber.toString()
        );
    }
    
    mapToDDFromCategory(docCategory: DocumentCategory) {
        return new DropDown(
            docCategory.documentCategoryId,
            docCategory.categoryTitle
        );
    }

    dateIdentifier(): string {
        return "filterDate" + this.id;
    }
}
