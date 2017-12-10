import { Component, OnInit } from '@angular/core';
import {AddressType} from '../addressType.model';
import {AddressTypeService} from '../../../services/addressType.service';
import {each} from 'lodash';
declare let $: any;

@Component({
  selector: 'app-address-type-new',
  templateUrl: './address-type-new.component.html',
  styleUrls: ['./address-type-new.component.scss']
})
export class AddressTypeNewComponent implements OnInit {

  addressType: AddressType;

  date_created: Date = new Date();
  date_modified: Date = new Date();
  is_deleted = false;

  constructor(private addressTypeService: AddressTypeService) {
    this.addressType = new AddressType();
    this.addressType.createdDate = this.date_created;
    this.addressType.modifiedDate = this.date_modified;
    this.addressType.isDeleted = this.is_deleted;
  }

  ngOnInit() {
  }

  onSubmit() {
    console.log('Usao');
    console.log(this.addressType);
    console.log('Prosao');

    this.addressTypeService.postAddressType(this.addressType).subscribe((r: any) => console.log('Post method addressType: ' + r),
      (error: any) => console.log('Error: ' + error.message));
  }

  newAddressType() {
    this.addressType = new AddressType();
  }

}
