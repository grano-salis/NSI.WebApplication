import { Component, OnInit, Input } from '@angular/core';

import {CaseCategory} from './caseCategory.model';
import {CaseCategoryService} from '../../services/caseCategory.service';
import {Observable} from 'rxjs/Observable';
import { Response } from '@angular/http';
import { DatePipe } from '@angular/common/src/pipes';
import { DateFormatter } from 'ngx-bootstrap/datepicker/date-formatter';
import { DatePickerComponent } from 'ngx-bootstrap/datepicker/datepicker.component';
import { DatepickerConfig } from 'ngx-bootstrap/datepicker/datepicker.config';
import { DatepickerModule } from 'ngx-bootstrap/datepicker/datepicker.module';
import { DatePickerInnerComponent } from 'ngx-bootstrap/datepicker/datepicker-inner.component';
import { AlertService } from '../../services/alert.service';

declare var $: any;

@Component({
  selector: 'app-caseCategory',
  templateUrl: './caseCategory.component.html',
  styleUrls: ['./caseCategory.component.scss']
})
export class CaseCategoryComponent implements OnInit {
  caseCategories:CaseCategory[]=[];
  caseCategory:CaseCategory=new CaseCategory();
  
  selectedCaseCategory:CaseCategory;
  submitted = false;
  editcc:CaseCategory=new CaseCategory();
  sortBy='';
  filterName:String="";
  filteredList:CaseCategory[]=[];
  
  
  
  constructor(private caseCategoryService: CaseCategoryService, private alertService: AlertService) { }

  ngOnInit() {
    
    this.getCaseCategories();
    console.log(this.filteredList);
    console.log(this.caseCategories);
  }
  onSubmit() { 
    this.caseCategory.customerId=1;
    this.postCaseCategory();
    
    this.caseCategory=new CaseCategory();
   }
  
  getCaseCategories()
  {
    this.caseCategoryService.getCaseCategories()
    .subscribe(
      response=>{
      this.caseCategories=response;
      this.filteredList=this.caseCategories;
      console.log(this.caseCategories);
      console.log(this.filteredList);
    },
    (error)=>console.log("Error getCaseCategories: "+error)
    );
  }
  getCaseCategoryById(id:number)
  {
    this.caseCategoryService.getCaseCategoryById(id)
    .subscribe(
      response=>{
      this.caseCategory=response.data;
      console.log(this.caseCategory.dateCreated);},
    (error)=>console.log(error)
    );
  }
  postCaseCategory()
  {
    this.caseCategoryService.postCaseCategory(this.caseCategory).subscribe((r: any) => {console.log( r),this.getCaseCategories(),this.alertService.showSuccess("New case category added.")},
    (error: any) =>{ console.log("Error: " + error.message), this.alertService.showError(error.error)});
  }
  deleteCaseCategory()
  {
    this.caseCategoryService.deleteCaseCategory(this.selectedCaseCategory.caseCategoryId).subscribe((r: any) => {
      let index = this.caseCategories.findIndex(d => d.caseCategoryId === this.selectedCaseCategory.caseCategoryId);
      this.caseCategories.splice(index, 1);
      this.filteredList=this.caseCategories;      
      this.alertService.showSuccess("Case category deleted")},
    (error: any) => {console.log("Error: " + error.message);
  this.alertService.showError(error.error)});

  
  }
  /*putCaseCategory(id:number,caseCategory:CaseCategory)
  {
    console.log(caseCategory.dateCreated);
    this.caseCategoryService.putCaseCategory(id, caseCategory).subscribe((r: any) =>{ console.log('Saljemo update: ' + r),this.alertService.showSuccess("Successfully changed case category")},
    (error: any) => {console.log("Error: " + error.message),this.alertService.showError(error.error)});
  }*/
  onSelect(caseCategory:CaseCategory)
  {
    this.editcc=    Object.assign({}, caseCategory);
    this.selectedCaseCategory=caseCategory;

  }
  sc()
  {
    this.caseCategoryService.putCaseCategory(this.editcc.caseCategoryId,this.editcc)
    .subscribe(
      response=>{
       // this.getCaseCategoryById(this.editcc.caseCategoryId);
        this.selectedCaseCategory=null;
        this.getCaseCategories();
        this.alertService.showSuccess("Successfully changed case category.")  
    },
    (error)=>{   // this.selectedCaseCategory=this.editcc;
      console.log("Error putCaseCategory: "+error),
    this.alertService.showError(error.error)}
    );
    
  }
  c()
  {
    this.selectedCaseCategory=this.editcc;
    
    this.selectedCaseCategory=null;
  }

  sort(sortBy:String)
  {
    if(sortBy=='caseCategoryName')
    {
    this.caseCategories.sort((l,r):number=>
  {
    if(l.caseCategoryName.toLocaleLowerCase()<r.caseCategoryName.toLocaleLowerCase()) return -1;
    if(l.caseCategoryName.toLocaleLowerCase()>r.caseCategoryName.toLocaleLowerCase()) return 1;
    return 0;
  });
  this.sortBy='Case category name (A-Z)';
  }

  if(sortBy=='-caseCategoryName')
  {
  this.caseCategories.sort((l,r):number=>
{
  if(l.caseCategoryName.toLocaleLowerCase()>r.caseCategoryName.toLocaleLowerCase()) return -1;
  if(l.caseCategoryName.toLocaleLowerCase()<r.caseCategoryName.toLocaleLowerCase()) return 1;
  return 0;
});
this.sortBy='Case category name (Z-A)';
}
  if(sortBy=='dateCreated')
  {
  this.caseCategories.sort((l,r):number=>
{
  if(l.dateCreated<r.dateCreated) return -1;
  if(l.dateCreated>r.dateCreated) return 1;
  return 0;
});
this.sortBy='Date created (oldest-newest)';
}

if(sortBy=='-dateCreated')
{
this.caseCategories.sort((l,r):number=>
{
if(l.dateCreated>r.dateCreated) return -1;
if(l.dateCreated<r.dateCreated) return 1;
return 0;
});
this.sortBy='Date created (newest-oldest)';
}
if(sortBy=='dateModified')
{
this.caseCategories.sort((l,r):number=>
{
if(l.dateModified<r.dateModified) return -1;
if(l.dateModified>r.dateModified) return 1;
return 0;
});
this.sortBy='Date modified (oldest-newest)';
}

if(sortBy=='-dateModified')
{
this.caseCategories.sort((l,r):number=>
{
if(l.dateModified>r.dateModified) return -1;
if(l.dateModified<r.dateModified) return 1;
return 0;
});
this.sortBy='Date modified (newest-oldest)';
}

this.filteredList=this.caseCategories;
this.filterName='';
}

  sort1()
  {
    this.caseCategories
    this.caseCategories.sort((l,r):number=>
  {
    /**if(l.caseCategoryName<r.caseCategoryName) return -1;
    
    if(l.caseCategoryName>r.caseCategoryName) return 1;*/
    if(l.dateCreated<r.dateCreated) return -1;
    
    if(l.dateCreated>r.dateCreated) return 1;

    return 0;

  });
  }

  search(filterName:String)
  {

    console.log("Filter results:",this.filterName);
   this.filteredList=this.caseCategories.filter(function(item:CaseCategory){
    return item.caseCategoryName.toLowerCase().includes(filterName.toLowerCase());
    
  
});
    console.log("Filter results:",this.filteredList);
  }
  

}
