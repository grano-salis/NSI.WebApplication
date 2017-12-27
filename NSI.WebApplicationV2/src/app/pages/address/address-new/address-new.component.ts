import { Component, OnInit } from '@angular/core';
import {Address} from '../address.model';
import {AddressService} from '../../../services/address.service';
import {AddressType} from '../addressType.model';
import {AddressTypeService} from '../../../services/addressType.service';
import { Router } from '@angular/router';
declare var google: any;

@Component({
  selector: 'app-address-new',
  // template: '<ng4geo-autocomplete (componentCallback)="autoCompleteCallback1($event)"></ng4geo-autocomplete>',
  templateUrl: './address-new.component.html',
  styleUrls: ['./address-new.component.scss']
})


export class AddressNewComponent implements OnInit {
  _router: Router;

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

  constructor(private addressService: AddressService, private addressTypeService: AddressTypeService, private router: Router) {
    this.address = new Address();
    this.address.dateCreated = this.date_created;
    this.address.dateModified = this.date_modified;
    this.address.isDeleted = this.is_deleted;
    this._router = router;
  }

  ngOnInit() {
    this.addressTypeService.getAddressTypes().subscribe((addressTypes: any) => {
      this.addressTypes = addressTypes;
    });
        let mapProp = {
        center: new google.maps.LatLng(0.0, 0.0),
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    let map = new google.maps.Map(document.getElementById("map"), mapProp);
  }

  initMap(lat: number, lng: number) {
    console.log(lat)
    console.log(lng)
            let mapProp = {
            center: new google.maps.LatLng(lat, lng),
            zoom: 15,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        let map = new google.maps.Map(document.getElementById("map"), mapProp);

        var marker = new google.maps.Marker({
          map: map,
          position: new google.maps.LatLng(lat, lng),
          title: this.address.address1
        });
  }

  onSubmit() {
    console.log('Usao');
    console.log(this.address);
    console.log('Prosao');

    this.addressService.postAddress(this.address).subscribe((r: any) => console.log('Post method address: ' + r),
      (error: any) => console.log('Error: ' + error.message));
    this._router.navigateByUrl('address/list');

  }

  newAddress() {
    this.address = new Address();
  }

  autoCompleteCallback1(data: any): any {
    this.componentData1 = JSON.stringify(data);

    if (data.response === true) {
      console.log("Usao");
      console.log(data.data)
      this.address.address1 = data.data.address_components[0].long_name;
      this.address.city = String(data.data.address_components[2].long_name);
      if(data.data.address_components[6] != null){
      this.address.zipCode = data.data.address_components[6].long_name;
      }
      else {
      this.address.zipCode = data.data.address_components[5].long_name;
      }

       let lat = data.data.geometry.location.lat;
       let lng = data.data.geometry.location.lng;

      this.initMap(lat,lng);

      console.log(this.address.address1 );
      console.log(this.address.city );
      console.log(this.address.zipCode );
    }
  }
}
