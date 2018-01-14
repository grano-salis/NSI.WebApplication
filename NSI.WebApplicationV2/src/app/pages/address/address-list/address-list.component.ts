import { Component, OnInit } from "@angular/core";
import { Address } from "../address.model";
import { AddressService } from "../../../services/address.service";
import { each } from "lodash";
import { AddressTypeService } from "../../../services/addressType.service";
import { AddressType } from "../addressType.model";
import { Router } from "@angular/router";
import { AddressSearchCriteria } from "../addressSearchCriteria.model";
import { forEach } from "@angular/router/src/utils/collection";
declare var $: any;
declare var google: any;

@Component({
  selector: "app-address-list",
  templateUrl: "./address-list.component.html",
  styleUrls: ["./address-list.component.scss"]
})
export class AddressListComponent implements OnInit {
  _router: Router;
  addresses: Address[];
  filteredItems: Address[];
  pages: number = 4;
  pageSize: number = 5;
  pageNumber: number = 0;
  currentIndex: number = 1;
  pagesIndex: Array<number>;
  pageStart: number = 1;
  inputName: string = "";
  searchText: String;

  temp_addressType: AddressType;

  constructor(private addressService: AddressService, private addressTypeService: AddressTypeService, router: Router) {
    this._router = router;     
  }

  ngOnInit() {
    this.loadAddresses();
    this.addressTypeService.getAddressTypes().subscribe((addressTypes: any) => {
    this.addressTypes = addressTypes;       
    });
    
  }

  loadAddresses(): any {
    this.addressService.getAddreses().subscribe((addresses: Address[]) => {
      this.addresses = addresses.filter(a => a.isDeleted == false);
      this.filteredItems = this.addresses.filter(a => a.isDeleted == false);  
      this.init();    
    });
  }

  _address: Address = new Address();
  addressTypes: AddressType[];

  public componentData1: any = "";
  public userSettings: any = {
    resOnSearchButtonClickOnly: false,
    inputPlaceholderText: "Start typing address",
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
    this.initMap(0, 0);
  }

  onCloseEditHandled() {
    this.displayEdit = "none";
    this.componentData1 = null;
  }

  onCloseDeleteHandled() {
    this.displayDelete = "none";
    this.componentData1 = null;
  }

  openRemoveModal(address: Address) {
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
    console.log(lat);
    console.log(lng);
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
      console.log(data.data);
      this._address.address1 = data.data.address_components[0].long_name;
      this._address.city = String(data.data.address_components[2].long_name);
      if (data.data.address_components[6] != null) {
        this._address.zipCode = data.data.address_components[6].long_name;
      } else {
        this._address.zipCode = data.data.address_components[5].long_name;
      }

      let lat = data.data.geometry.location.lat;
      let lng = data.data.geometry.location.lng;

      this.initMap(lat, lng);

      console.log(this._address.address1);
      console.log(this._address.city);
      console.log(this._address.zipCode);
    }
  }

  onSubmit() {
    this.addressService
      .updateAddress(this._address)
      .subscribe(
        (r: any) => console.log("Put method address: " + r),
        (error: any) => console.log("Error: " + error.message)
      );
    this.displayEdit = "none";
  }

  deleteAddress() {
    this.addressService
      .deleteAddress(this._address)
      .subscribe(
        (r: any) => console.log("Delete method address: " + r),
        (error: any) => console.log("Error: " + error.message)
      );

    let index = this.addresses.indexOf(this._address);

    if (index != -1) {
      this.addresses.splice(index, 1);
    }

    this.displayDelete = "none";

    //$(document).ready(()=> location.reload());
  }

  init() {
    this.currentIndex = 1;
    this.pageStart = 1;
    this.pages = 4;

    this.pageNumber = parseInt("" + this.filteredItems.length / this.pageSize);
    if (this.filteredItems.length % this.pageSize != 0) {
      this.pageNumber++;
    }

    if (this.pageNumber < this.pages) {
      this.pages = this.pageNumber;
    }

    this.refreshItems();
    console.log("this.pageNumber :  " + this.pageNumber);
  }

  FilterByName() {
    this.filteredItems = [];
    if (this.inputName != "") {
      this.addresses.forEach(element => {
        if (element.address1.toUpperCase().indexOf(this.inputName.toUpperCase()) >= 0) {
          this.filteredItems.push(element);
        }
      });
    } else {
      this.filteredItems = this.addresses;
    }
    console.log(this.filteredItems);
    this.init();
  }
  
  fillArray(): any {
    var obj = new Array();
    for (var index = this.pageStart; index < this.pageStart + this.pages; index++) {
      obj.push(index);
    }
    return obj;
  }

  refreshItems() {
    this.addresses = this.filteredItems.slice((this.currentIndex - 1) * this.pageSize, this.currentIndex * this.pageSize);
    console.log('items size:'+ this.addresses.length);
    this.pagesIndex = this.fillArray();
  }

  prevPage() {
    if (this.currentIndex > 1) {
      this.currentIndex--;
    }
    if (this.currentIndex < this.pageStart) {
      this.pageStart = this.currentIndex;
    }
    this.refreshItems();
  }

  nextPage() {
    if (this.currentIndex < this.pageNumber) {
      this.currentIndex++;
    }
    if (this.currentIndex >= this.pageStart + this.pages) {
      this.pageStart = this.currentIndex - this.pages + 1;
    }

    this.refreshItems();
  }

  setPage(index: number) {
    this.currentIndex = index;
    this.refreshItems();
  }

  getSortedAddresses(criteria: AddressSearchCriteria){
    this.addresses = this.addressService.getSortedAddresses(criteria, this.addresses);
  }
 
 onSorted($event: any){
   this.getSortedAddresses($event);
  }
}
