import { Component, OnInit, Input } from '@angular/core';

import {ClientTypeService} from '../../services/clientType.service';
import {Observable} from 'rxjs/Observable';
import { ClientType } from './clientType.model';
import { AlertService } from '../../services/alert.service';

@Component({
  selector: 'app-clientType',
  templateUrl: './clientType.component.html',
  styleUrls: []
})
export class ClientTypeComponent implements OnInit {

  clientTypes:ClientType[]=[];
  clientType:ClientType=new ClientType();
  
  selectedClientType:ClientType;
  
  editcc:ClientType=new ClientType();
  sortBy='';
  filterName:String="";
  filteredList:ClientType[]=[];
  
  
  
  constructor(private clientTypeService: ClientTypeService, private alertService: AlertService) { }

  ngOnInit() {
    
    this.getClientTypes();
    console.log(this.filteredList);
    console.log(this.clientTypes);
  }
  onSubmit() { 
    this.clientType.dateCreated=new Date();
    this.clientType.customerId=1;
    this.postClientType();
    this.clientType=new ClientType();
       }
  
  getClientTypes()
  {
    this.clientTypeService.getClientTypes()
    .subscribe(
      response=>{
      this.clientTypes=response;
      this.filteredList=this.clientTypes;
      console.log(this.clientTypes);
      console.log(this.filteredList);
    },
    (error)=>console.log("Error getClientTypes: "+error)
    );
  }
  getclientTypeById(id:number)
  {
    this.clientTypeService.getClientTypeById(id)
    .subscribe(
      response=>{
      this.clientType=response.data;
      console.log(this.clientType.dateCreated);},
    (error)=>console.log(error)
    );
  }
  postClientType()
  {

    this.clientTypeService.postClientType(this.clientType).subscribe((r: any) => {console.log( r),this.getClientTypes(),this.alertService.showSuccess("New client type added.")},
    (error: any) =>{ console.log("Error: " + error.message), this.alertService.showError(error.error)});
  }
  deleteClientType()
  {
    this.clientTypeService.deleteClientType(this.selectedClientType.clientTypeId).subscribe((r: any) => {
      let index = this.clientTypes.findIndex(d => d.clientTypeId === this.selectedClientType.clientTypeId);
      this.clientTypes.splice(index, 1);
      this.filteredList=this.clientTypes; this.alertService.showSuccess("Client type deleted")},
    (error: any) => {console.log("Error: " + error.message);
  this.alertService.showError(error.error)});
  }
  putClientType(id:number,ClientType:ClientType)
  {
    console.log(ClientType.dateCreated);
    this.clientTypeService.putClientType(id, ClientType).subscribe((r: any) =>{ console.log('Saljemo update: ' + r),this.alertService.showSuccess("Successfully changed client type")},
    (error: any) => {console.log("Error: " + error.message),this.alertService.showError(error.error)});
  }
  onSelect(clientType:ClientType)
  {
    this.editcc=    Object.assign({}, clientType);
    this.selectedClientType=clientType;

  }
  sc()
  {
    this.clientTypeService.putClientType(this.editcc.clientTypeId,this.editcc)
    .subscribe(
      response=>{
        this.selectedClientType=null;
        this.getClientTypes();
        this.alertService.showSuccess("Successfully changed client type.")  
    },
    (error)=>{   // this.selectedclientType=this.editcc;
      console.log("Error putclientType: "+error),
    this.alertService.showError(error.error)}
    );
    
  }
  c()
  {
    this.selectedClientType=this.editcc;
    
    this.selectedClientType=null;
  }

  sort(sortBy:String)
  {
    if(sortBy=='clientTypeName')
    {
    this.clientTypes.sort((l,r):number=>
  {
    if(l.clientTypeName.toLocaleLowerCase()<r.clientTypeName.toLocaleLowerCase()) return -1;
    if(l.clientTypeName.toLocaleLowerCase()>r.clientTypeName.toLocaleLowerCase()) return 1;
    return 0;
  });
  this.sortBy='Client type name (A-Z)';
  }

  if(sortBy=='-clientTypeName')
  {
  this.clientTypes.sort((l,r):number=>
{
  if(l.clientTypeName.toLocaleLowerCase()>r.clientTypeName.toLocaleLowerCase()) return -1;
  if(l.clientTypeName.toLocaleLowerCase()<r.clientTypeName.toLocaleLowerCase()) return 1;
  return 0;
});
this.sortBy='Client type name (Z-A)';
}
  if(sortBy=='dateCreated')
  {
  this.clientTypes.sort((l,r):number=>
{
  if(l.dateCreated<r.dateCreated) return -1;
  if(l.dateCreated>r.dateCreated) return 1;
  return 0;
});
this.sortBy='Date created (oldest-newest)';
}

if(sortBy=='-dateCreated')
{
this.clientTypes.sort((l,r):number=>
{
if(l.dateCreated>r.dateCreated) return -1;
if(l.dateCreated<r.dateCreated) return 1;
return 0;
});
this.sortBy='Date created (newest-oldest)';
}
if(sortBy=='dateModified')
{
this.clientTypes.sort((l,r):number=>
{
if(l.dateModified<r.dateModified) return -1;
if(l.dateModified>r.dateModified) return 1;
return 0;
});
this.sortBy='Date modified (oldest-newest)';
}

if(sortBy=='-dateModified')
{
this.clientTypes.sort((l,r):number=>
{
if(l.dateModified>r.dateModified) return -1;
if(l.dateModified<r.dateModified) return 1;
return 0;
});
this.sortBy='Date modified (newest-oldest)';
}

this.filteredList=this.clientTypes;
this.filterName='';
}


  search(filterName:String)
  {

    console.log("Filter results:",this.filterName);
   this.filteredList=this.clientTypes.filter(function(item:ClientType){
    return item.clientTypeName.toLowerCase().includes(filterName.toLowerCase());
    
  
});
    console.log("Filter results:",this.filteredList);
  }
  

}



