import { Component, OnInit, Input } from '@angular/core';
import { FileType } from './file-type.model';
import { FileTypeService } from '../../services/file-type.service';
import { AlertService } from '../../services/alert.service';

@Component({
  selector: 'app-file-type',
  templateUrl: './file-type.component.html',
  styleUrls: ['./file-type.component.scss']
})
export class FileTypeComponent implements OnInit {
  
    fileTypes:FileType[]=[];
    fileType:FileType=new FileType();
    
    selectedFileType:FileType;
    submitted = false;
    editcc:FileType=new FileType();
    sortBy='';
    filterName:String="";
    filteredList:FileType[]=[];
    
    
    
    constructor(private FileTypeService: FileTypeService, private alertService: AlertService) { }
  
    ngOnInit() {
      
      this.getFileTypes();
      console.log(this.filteredList);
      console.log(this.fileTypes);
    }
    onSubmit() { 
      //this.fileType.dateCreated=new Date();
      this.fileType.iconPath='fa-file-'+this.fileType.extension+'-o';
      this.postFileType();
      this.fileType=new FileType();
         }
    
    getFileTypes()
    {
      this.FileTypeService.getFileTypes()
      .subscribe(
        response=>{
        this.fileTypes=response;
        this.filteredList=this.fileTypes;
        console.log(this.fileTypes);
        console.log(this.filteredList);
      },
      (error)=>console.log("Error getFileTypes: "+error)
      );
    }
    getFileTypeById(id:number)
    {
      this.FileTypeService.getFileTypeById(id)
      .subscribe(
        response=>{
        this.fileType=response.data;
        console.log(this.fileType.dateCreated);},
      (error)=>console.log(error)
      );
    }
    postFileType()
    {
  
      this.FileTypeService.postFileType(this.fileType).subscribe((r: any) => {console.log( r),this.getFileTypes(),this.alertService.showSuccess("New file type added.")},
      (error: any) =>{ console.log("Error: " + error.message), this.alertService.showError(error.error)});
    }
    deleteFileType()
    {
      this.FileTypeService.deleteFileType(this.selectedFileType.fileTypeId).subscribe((r: any) => {
        let index = this.fileTypes.findIndex(d => d.fileTypeId === this.selectedFileType.fileTypeId);
        this.fileTypes.splice(index, 1);
        this.filteredList=this.fileTypes; 
        this.alertService.showSuccess("File type deleted")},
      (error: any) => {console.log("Error: " + error.message);
    this.alertService.showError(error.error)});
    }
    
    putFileType(id:number,FileType:FileType)
    {
      console.log(FileType.dateCreated);
      this.FileTypeService.putFileType(id, FileType).subscribe((r: any) =>{ console.log('Saljemo update: ' + r),this.alertService.showSuccess("Successfully changed file type")},
      (error: any) => {console.log("Error: " + error.message),this.alertService.showError(error.error)});
    }
    onSelect(fileType:FileType)
    {
      this.editcc=    Object.assign({}, fileType);
      this.selectedFileType=fileType;
  
    }
    sc()
    {
  
      this.FileTypeService.putFileType(this.editcc.fileTypeId,this.editcc)
      .subscribe(
        response=>{
          this.selectedFileType=null;
          this.getFileTypes();
          this.alertService.showSuccess("Successfully changed file type.")  
      },
      (error)=>{   // this.selectedFileType=this.editcc;
        console.log("Error putFileType: "+error),
      this.alertService.showError(error.error)}
      );
      
    }
    c()
    {
      this.selectedFileType=this.editcc;
      
      this.selectedFileType=null;
    }
  
    sort(sortBy:String)
    {
      if(sortBy=='extension')
      {
      this.fileTypes.sort((l,r):number=>
    {
      if(l.extension.toLocaleLowerCase()<r.extension.toLocaleLowerCase()) return -1;
      if(l.extension.toLocaleLowerCase()>r.extension.toLocaleLowerCase()) return 1;
      return 0;
    });
    this.sortBy='Extension (A-Z)';
    }
  
    if(sortBy=='-extension')
    {
    this.fileTypes.sort((l,r):number=>
  {
    if(l.extension.toLocaleLowerCase()>r.extension.toLocaleLowerCase()) return -1;
    if(l.extension.toLocaleLowerCase()<r.extension.toLocaleLowerCase()) return 1;
    return 0;
  });
  this.sortBy='Extension (Z-A)';
  }
    if(sortBy=='dateCreated')
    {
    this.fileTypes.sort((l,r):number=>
  {
    if(l.dateCreated<r.dateCreated) return -1;
    if(l.dateCreated>r.dateCreated) return 1;
    return 0;
  });
  this.sortBy='Date created (oldest-newest)';
  }
  
  if(sortBy=='-dateCreated')
  {
  this.fileTypes.sort((l,r):number=>
  {
  if(l.dateCreated>r.dateCreated) return -1;
  if(l.dateCreated<r.dateCreated) return 1;
  return 0;
  });
  this.sortBy='Date created (newest-oldest)';
  }
  if(sortBy=='dateModified')
  {
  this.fileTypes.sort((l,r):number=>
  {
  if(l.dateModified<r.dateModified) return -1;
  if(l.dateModified>r.dateModified) return 1;
  return 0;
  });
  this.sortBy='Date modified (oldest-newest)';
  }
  
  if(sortBy=='-dateModified')
  {
  this.fileTypes.sort((l,r):number=>
  {
  if(l.dateModified>r.dateModified) return -1;
  if(l.dateModified<r.dateModified) return 1;
  return 0;
  });
  this.sortBy='Date modified (newest-oldest)';
  }
  
  this.filteredList=this.fileTypes;
  this.filterName='';
  }
  
  
    search(filterName:String)
    {
  
      console.log("Filter results:",this.filterName);
     this.filteredList=this.fileTypes.filter(function(item:FileType){
      return item.extension.toLowerCase().includes(filterName.toLowerCase());
      
    
  });
      console.log("Filter results:",this.filteredList);
    }
    
  
  }
  
  
  
  
