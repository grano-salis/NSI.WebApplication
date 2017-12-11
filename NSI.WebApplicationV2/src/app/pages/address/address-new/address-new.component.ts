import { Component, OnInit } from '@angular/core';
import {Address} from '../address.model';
import {AddressService} from '../../../services/address.service';
import { Ng4GeoautocompleteModule } from 'ng4-geoautocomplete';
import {AddressType} from '../addressType.model';
import {AddressTypeService} from '../../../services/addressType.service';

@Component({
  selector: 'app-address-new',
  // template: '<ng4geo-autocomplete (componentCallback)="autoCompleteCallback1($event)"></ng4geo-autocomplete>',
  templateUrl: './address-new.component.html',
  styleUrls: ['./address-new.component.scss']
})
export class AddressNewComponent implements OnInit {

  address: Address;
  addressTypes: AddressType[];

  date_created: Date = new Date();
  date_modified: Date = new Date();
  is_deleted = false;

  // Dio za autocomplete
  public componentData1: any = '';
  public userSettings: any = {
    resOnSearchButtonClickOnly: false,
    inputPlaceholderText: 'Start typing address',
    showSearchButton: false,
    showRecentSearch: false,
    showCurrentLocation: false
  };

  constructor(private addressService: AddressService, private addressTypeService: AddressTypeService) {
    this.address = new Address();
    this.address.dateCreated = this.date_created;
    this.address.dateModified = this.date_modified;
    this.address.isDeleted = this.is_deleted;
  }

  ngOnInit() {
    this.addressTypeService.getAddressTypes().subscribe((addressTypes: any) => {
      this.addressTypes = addressTypes;
    });
  }

  onSubmit() {
    console.log('Usao');
    console.log(this.address);
    console.log('Prosao');

    this.addressService.postAddress(this.address).subscribe((r: any) => console.log('Post method address: ' + r),
      (error: any) => console.log('Error: ' + error.message));
  }

  newAddress() {
    this.address = new Address();
  }

  autoCompleteCallback1(data: any): any {
    this.componentData1 = JSON.stringify(data);

    if (data.response === true) {
      console.log("Usao");

      this.address.address1 = data.data.address_components[0].long_name;
      this.address.city = String(data.data.address_components[2].long_name);
      this.address.zipCode = +data.data.address_components[6].long_name;


      console.log(this.address.address1 );
      console.log(this.address.city );
      console.log(this.address.zipCode );
    }
  }
}
