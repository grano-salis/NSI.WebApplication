import { Component, OnInit, Input } from '@angular/core';
import { DocumentCategory } from './document-category.model';
import { DocumentCategoryService } from '../../services/document-category.service';
import { AlertService } from '../../services/alert.service';

@Component({
  selector: 'app-document-category',
  templateUrl: './document-category.component.html',
  styleUrls: ['./document-category.component.scss']
})

export class DocumentCategoryComponent implements OnInit {
  documentCategories:DocumentCategory[]=[];
  documentCategory:DocumentCategory=new DocumentCategory();
  
  selectedDocumentCategory:DocumentCategory;
  
  editcc:DocumentCategory=new DocumentCategory();
  sortBy='';
  filterName:String="";
  filteredList:DocumentCategory[]=[];
  
  
  
  constructor(private DocumentCategoryService: DocumentCategoryService, private alertService: AlertService) { }

  ngOnInit() {
    
    this.getDocumentCategories();
    console.log(this.filteredList);
    console.log(this.documentCategories);
  }
  onSubmit() { 
    //this.DocumentCategory.DocumentCategoryId=3;
    this.documentCategory.dateCreated=new Date();
    //this.DocumentCategory.isDeleted=false;
    this.documentCategory.customerId=1;
    this.postDocumentCategory();
    this.documentCategory=new DocumentCategory();
    
   }
  
  getDocumentCategories()
  {
    this.DocumentCategoryService.getDocumentCategories()
    .subscribe(
      response=>{
      this.documentCategories=response;
      this.filteredList=this.documentCategories;
      console.log(this.documentCategories);
      console.log(this.filteredList);
    },
    (error)=>console.log("Error getDocumentCategories: "+error)
    );
  }
  getDocumentCategoryById(id:number)
  {
    this.DocumentCategoryService.getDocumentCategoryById(id)
    .subscribe(
      response=>{
      this.documentCategory=response.data;
      console.log(this.documentCategory.dateCreated);},
    (error)=>console.log(error)
    );
  }
  postDocumentCategory()
  {

    this.DocumentCategoryService.postDocumentCategory(this.documentCategory).subscribe((r: any) => {console.log( r),this.getDocumentCategories(),this.alertService.showSuccess("New document category added.")},
    (error: any) =>{ console.log("Error: " + error.message), this.alertService.showError(error.error)});
  }
  deleteDocumentCategory()
  {
    this.DocumentCategoryService.deleteDocumentCategory(this.selectedDocumentCategory.documentCategoryId).subscribe((r: any) => {
      let index = this.documentCategories.findIndex(d => d.documentCategoryId === this.selectedDocumentCategory.documentCategoryId);
      this.documentCategories.splice(index, 1);
      this.filteredList=this.documentCategories; 
      this.alertService.showSuccess("document category deleted")},
    (error: any) => {console.log("Error: " + error.message);
  this.alertService.showError(error.error)});
  }
  putDocumentCategory(id:number,DocumentCategory:DocumentCategory)
  {
    console.log(DocumentCategory.dateCreated);
    this.DocumentCategoryService.putDocumentCategory(id, DocumentCategory).subscribe((r: any) =>{ console.log('Saljemo update: ' + r),this.alertService.showSuccess("Successfully changed document category")},
    (error: any) => {console.log("Error: " + error.message),this.alertService.showError(error.error)});
  }
  onSelect(DocumentCategory:DocumentCategory)
  {
    this.editcc=    Object.assign({}, DocumentCategory);
    this.selectedDocumentCategory=DocumentCategory;

  }
  sc()
  {
    this.DocumentCategoryService.putDocumentCategory(this.editcc.documentCategoryId,this.editcc)
    .subscribe(
      response=>{
        this.selectedDocumentCategory=null;
        this.getDocumentCategories();
        this.alertService.showSuccess("Successfully changed document category.")  
    },
    (error)=>{   // this.selectedDocumentCategory=this.editcc;
      console.log("Error putDocumentCategory: "+error),
    this.alertService.showError(error.error)}
    );
    
  }
  c()
  {
    this.selectedDocumentCategory=this.editcc;
    
    this.selectedDocumentCategory=null;
  }

  sort(sortBy:String)
  {
    if(sortBy=='documentCategoryTitle')
    {
    this.documentCategories.sort((l,r):number=>
  {
    if(l.documentCategoryTitle.toLocaleLowerCase()<r.documentCategoryTitle.toLocaleLowerCase()) return -1;
    if(l.documentCategoryTitle.toLocaleLowerCase()>r.documentCategoryTitle.toLocaleLowerCase()) return 1;
    return 0;
  });
  this.sortBy='document category name (A-Z)';
  }

  if(sortBy=='-documentCategoryTitle')
  {
  this.documentCategories.sort((l,r):number=>
{
  if(l.documentCategoryTitle.toLocaleLowerCase()>r.documentCategoryTitle.toLocaleLowerCase()) return -1;
  if(l.documentCategoryTitle.toLocaleLowerCase()<r.documentCategoryTitle.toLocaleLowerCase()) return 1;
  return 0;
});
this.sortBy='Document category title (Z-A)';
}
  if(sortBy=='dateCreated')
  {
  this.documentCategories.sort((l,r):number=>
{
  if(l.dateCreated<r.dateCreated) return -1;
  if(l.dateCreated>r.dateCreated) return 1;
  return 0;
});
this.sortBy='Date created (oldest-newest)';
}

if(sortBy=='-dateCreated')
{
this.documentCategories.sort((l,r):number=>
{
if(l.dateCreated>r.dateCreated) return -1;
if(l.dateCreated<r.dateCreated) return 1;
return 0;
});
this.sortBy='Date created (newest-oldest)';
}
if(sortBy=='dateModified')
{
this.documentCategories.sort((l,r):number=>
{
if(l.dateModified<r.dateModified) return -1;
if(l.dateModified>r.dateModified) return 1;
return 0;
});
this.sortBy='Date modified (oldest-newest)';
}

if(sortBy=='-dateModified')
{
this.documentCategories.sort((l,r):number=>
{
if(l.dateModified>r.dateModified) return -1;
if(l.dateModified<r.dateModified) return 1;
return 0;
});
this.sortBy='Date modified (newest-oldest)';
}

this.filteredList=this.documentCategories;
this.filterName='';
}


  search(filterName:String)
  {

    console.log("Filter results:",this.filterName);
   this.filteredList=this.documentCategories.filter(function(item:DocumentCategory){
    return item.documentCategoryTitle.toLowerCase().includes(filterName.toLowerCase());
    
  
});
    console.log("Filter results:",this.filteredList);
  }
  

}

