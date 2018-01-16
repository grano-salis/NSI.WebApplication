import { Component, OnInit } from '@angular/core';
import {AddressType} from '../addressType.model';
import {AddressTypeService} from '../../../services/addressType.service';
import {each} from 'lodash';
import { Router } from '@angular/router';
declare let $: any;

@Component({
  selector: 'app-address-type-new',
  templateUrl: './address-type-new.component.html',
  styleUrls: ['./address-type-new.component.scss']
})
export class AddressTypeNewComponent implements OnInit {
  _router: Router;

  addressType: AddressType;

  date_created: Date = new Date();
  date_modified: Date = new Date();
  is_deleted = false;
  customer_id = 1;

  constructor(private addressTypeService: AddressTypeService, private router: Router) {
    this.addressType = new AddressType();
    this.addressType.createdDate = this.date_created;
    this.addressType.modifiedDate = this.date_modified;
    this.addressType.isDeleted = this.is_deleted;
    this.addressType.customerId = this.customer_id;
    this._router = router;
  }

  ngOnInit() {
  }

  routeWithDelay(){
    setTimeout(() => {
        this._router.navigate(['address/type/list'])
      }
      , 1000);  
    }

  onSubmit() {
    console.log(this.addressType);
    this.addressTypeService.postAddressType(this.addressType).subscribe((r: any) => console.log('Post method addressType: ' + r),
      (error: any) => console.log('Error: ' + error.message));
      
  this.routeWithDelay();      
}



  newAddressType() {
    this.addressType = new AddressType();
  }

}
