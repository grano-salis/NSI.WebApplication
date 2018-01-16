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

declare var $: any;

@Component({
  selector: 'app-caseCategory',
  templateUrl: './caseCategory.component.html',
  styleUrls: ['./caseCategory.component.scss']
})
export class CaseCategoryComponent implements OnInit {
  caseCategories:CaseCategory[]=[];
  caseCategory:CaseCategory=new CaseCategory();
  
  tekst='';
  @Input('scc')
  selectedCaseCategory:CaseCategory;
  submitted = false;
  editForm=false;
  editcc:CaseCategory=new CaseCategory();
  sortBy='Date created';
  filterName:String="";
  filteredList:CaseCategory[]=[];
  
  
  constructor(private caseCategoryService: CaseCategoryService) { }

  ngOnInit() {
    this.editForm=false;
    
    this.getCaseCategories();
    /*this.caseCategory.caseCategoryId=1;
    this.caseCategory.caseCategoryName="test";
    this.caseCategory.dateCreated=new Date();
    //this.caseCategory.dateModified=new Date();
    this.caseCategory.isDeleted=false;
    this.caseCategory.customerId=1;
    this.caseCategories.push(this.caseCategory);
    this.caseCategory=new CaseCategory();*/
    //this.filteredList=this.caseCategories;
    console.log(this.filteredList);
    console.log(this.caseCategories);
  }
  onSubmit() { 
    this.caseCategory.caseCategoryId=3;
    this.caseCategory.dateCreated=new Date();
    this.caseCategory.isDeleted=false;
    this.caseCategory.customerId=1;
    this.postCaseCategory();
    
    //this.caseCategories.push(this.caseCategory);
    this.caseCategory=new CaseCategory();
    
    
    
    //this.submitted = true;
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

    this.caseCategoryService.postCaseCategory(this.caseCategory).subscribe((r: any) => console.log( r),
    (error: any) => console.log("Error: " + error.message));
  }
  deleteCaseCategory(id:number)
  {
    this.caseCategoryService.deleteCaseCategory(id).subscribe((r: any) => this.getCaseCategories(),
    (error: any) => console.log("Error: " + error.message));
  }
  putCaseCategory(id:number,caseCategory:CaseCategory)
  {
  //caseCategory.caseCategoryDate =   new Date().toISOString();
  this.caseCategory.dateCreated =   new Date()
  
  
 //new DatePipe(Date.now().toString()).transform(value: any, format: "MM/DD/YYYY");
  
  
    console.log(caseCategory.dateCreated);
    this.caseCategoryService.putCaseCategory(id, caseCategory).subscribe((r: any) => console.log('Saljemo update: ' + r),
    (error: any) => console.log("Error: " + error.message));
  }
  onSelect(caseCategory:CaseCategory)
  {
    this.editcc=    Object.assign({}, caseCategory);
    this.selectedCaseCategory=caseCategory;

  }
  editCaseCategory(caseCategory:CaseCategory)
  {
    //this.selectedCaseCategory=caseCategory;
   this.editForm=!this.editForm;
   
  }
  sc()
  {
//this.selectedCaseCategory.caseCategoryName=this.editcc.caseCategoryName;
this.editcc.dateModified=new Date();
    this.caseCategoryService.putCaseCategory(this.editcc.caseCategoryId,this.editcc)
    .subscribe(
      response=>{
        this.selectedCaseCategory.caseCategoryName= this.editcc.caseCategoryName;
        this.selectedCaseCategory.dateModified= this.editcc.dateModified;        
        this.selectedCaseCategory=null;
    },
    (error)=>{   // this.selectedCaseCategory=this.editcc;
      console.log("Error putCaseCategory: "+error)}
    );



    //this.selectedCaseCategory.caseCategoryName=this.editcc.caseCategoryName;
    //this.caseCategoryService.putCaseCategory(this.selectedCaseCategory.caseCategoryId,this.selectedCaseCategory);
    /*
    this.editcc.caseCategoryId=5;
    //this.selectedCaseCategory.caseCategoryId=5;
    this.selectedCaseCategory.caseCategoryName=this.editcc.caseCategoryName;
    //=Object.assign({}, this.editcc);
    this.selectedCaseCategory=null;*/
    
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
    if(l.caseCategoryName<r.caseCategoryName) return -1;
    
    if(l.caseCategoryName>r.caseCategoryName) return 1;

    return 0;

  });
  this.sortBy='Case category name';
  
  }
  if(sortBy=='dateCreated')
  {
  this.caseCategories.sort((l,r):number=>
{
  if(l.dateCreated<r.dateCreated) return -1;
  
  if(l.dateCreated>r.dateCreated) return 1;

  return 0;

});

this.sortBy='Date created';
}
this.filteredList=this.caseCategories;
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

  search(filterName:string)
  {

    console.log("Filter results:",this.filterName);
   this.filteredList=this.caseCategories.filter(function(item:CaseCategory){
    return item.caseCategoryName.toLowerCase().includes(filterName.toLowerCase());
    
  
});
    console.log("Filter results:",this.filteredList);
  }



}
