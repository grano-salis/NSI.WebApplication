import { Component, OnInit } from "@angular/core";
import { Address } from "../address.model";
import { AddressService } from "../../../services/address.service";
import { each } from "lodash";
import { AddressTypeService } from "../../../services/addressType.service";
import { AddressType } from "../addressType.model";
declare let $: any;
declare var google: any;


@Component({
  selector: "app-address-list",
  templateUrl: "./address-list.component.html",
  styleUrls: ["./address-list.component.scss"]
})
export class AddressListComponent implements OnInit {
  addresses: Address[];

  constructor(private addressService: AddressService, private addressTypeService: AddressTypeService) {}

  ngOnInit() {
    this.loadAddresses();
    this.addressTypeService.getAddressTypes().subscribe((addressTypes: any) => {
      this.addressTypes = addressTypes;
    });
  }

  loadAddresses(): any {
    this.addressService.getAddreses().subscribe((addresses: any) => {
      this.addresses = addresses;
    });
  }

  _address: Address = new Address();
  addressTypes: AddressType[];

  public componentData1: any = '';
  public userSettings: any = {
    resOnSearchButtonClickOnly: false,
    inputPlaceholderText: 'Start typing address',
    showSearchButton: false,
    showRecentSearch: false,
    showCurrentLocation: false
  };

  displayEdit = "none";
  displayDelete = "none";

  openEditModal(address: Address) {
    this._address = address;
    this.displayEdit = "block";    
    this.showMap();
    this.initMap(0,0);
  }

  onCloseEditHandled() {
    this.displayEdit = "none";
    this.componentData1 = null;
  }

  onCloseDeleteHandled() {
    this.displayDelete = "none";
    this.componentData1 = null;
  }

  openRemoveModal(address: Address){
    this._address = address;
    this.displayDelete = "block";
  }

  showMap() {

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
          title: this._address.address1
        });           
  }

  autoCompleteCallback1(data: any): any {
    this.componentData1 = JSON.stringify(data);

    if (data.response === true) {
      console.log("Usao");
      console.log(data.data)
      this._address.address1 = data.data.address_components[0].long_name;
      this._address.city = String(data.data.address_components[2].long_name);
      if(data.data.address_components[6] != null){
      this._address.zipCode = data.data.address_components[6].long_name;
      }
      else {
      this._address.zipCode = data.data.address_components[5].long_name;
      }
      
       let lat = data.data.geometry.location.lat;
       let lng = data.data.geometry.location.lng;
      
      this.initMap(lat,lng);

      console.log(this._address.address1 );
      console.log(this._address.city );
      console.log(this._address.zipCode );
    }
  }

  onSubmit() {
    this.addressService.updateAddress(this._address).subscribe((r: any) => console.log('Put method address: ' + r),
      (error: any) => console.log('Error: ' + error.message));
      this.displayEdit = "none";
  }

  deleteAddress(){
    this.addressService.deleteAddress(this._address).subscribe((r: any) => console.log('Delete method address: ' + r),
    (error: any) => console.log('Error: ' + error.message));
    this.displayDelete = "none";
  }
}
