import {Component, Input, Output, EventEmitter} from '@angular/core';
import {AddressTypeService} from '../../../services/addressType.service';
import {AddressType} from '../addressType.model';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-address-type-modal-edit',
  templateUrl: './address-type-modal-edit.component.html',
  styleUrls: []
})
export class AddressTypeModalEditComponent {
  AddressTypeId: any;
  model: any;
  constructor(private router: Router, private route: ActivatedRoute, private addressTypeService: AddressTypeService) { }
   //@Input() address: AddressType;
  //AddressType: address= new AddressType;

  ngOnInit() {
    console.log('editAddressType ngOnInit uslo');
      this.model=new AddressType();
     this.AddressTypeId= +this.route.snapshot.paramMap.get('AddressTypeId');
    console.log('this.caseToEditId', this.AddressTypeId);
   // this.loadTypeAddress();
  }

  loadTypeAddress(){
    console.log('editAddressType loadTypeAddress uslo');
    //liniju ispod pada 
      this.addressTypeService.getAddressTypes(this.AddressTypeId).subscribe( data => {
        console.log('editAddressType loadTypeAddress uslo2');
        console.log('dataedit', data);
      this.model = data;
    });
    console.log('editAddressType loadTypeAddress proslo');
  }
  onSubmit() {
    console.log('editAddressType onSubmit uslo');
    console.log('this.model', this.model);
    this.addressTypeService.putAddressType(this.model.caseId, this.model).subscribe(data => {
      console.log('data', data);
     // this.router.navigate(['address-type-list/all']);
    });
  }

  
  
  /*
  editAddressType(address: AddressType){
     // this.addressTypeService.

    /*this.model.addressType.addressTypeName=address.addressTypeName;
    this.addressTypeService.postAddressType(this.model).subscribe(data => {
      console.log('data', data);
    });
     //console.log('editAddressType funkcija');
}*/

}
