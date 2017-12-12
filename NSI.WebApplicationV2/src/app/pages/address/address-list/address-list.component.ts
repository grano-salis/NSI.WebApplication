import { Component, OnInit } from '@angular/core';
import {Address} from '../address.model';
import {AddressService} from '../../../services/address.service';
import {each} from 'lodash';
declare let $: any;

@Component({
  selector: 'app-address-list',
  templateUrl: './address-list.component.html',
  styleUrls: ['./address-list.component.scss']
})
export class AddressListComponent implements OnInit {

  addresses: Address[];

  constructor(private addressService: AddressService) {
  }

  ngOnInit() {
    this.loadAddresses();
  }

  loadAddresses(): any {
    this.addressService.getAddreses().subscribe((addresses: any) => {
    this.addresses = addresses;
    });
  }

}
