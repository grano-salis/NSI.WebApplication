import { Component, OnInit } from '@angular/core';
import {Hearing} from '../../hearings/hearing-new/hearing';
import {Address} from '../address.model';
import {AddressService} from '../../../services/address.service';

@Component({
  selector: 'app-address-new',
  templateUrl: './address-new.component.html',
  styleUrls: ['./address-new.component.scss']
})
export class AddressNewComponent implements OnInit {

  address: Address;

  date_created: Date = new Date();
  date_modified: Date = new Date();
  is_deleted = false;

  constructor(private addressService: AddressService) {
    this.address = new Address();
    this.address.dateCreated = this.date_created;
    this.address.dateModified = this.date_modified;
    this.address.isDeleted = this.is_deleted;
  }

  ngOnInit() {
  }

  onSubmit() {
    console.log('Usao');
    console.log(this.address);
    console.log();

    this.addressService.postAddress(this.address).subscribe((r: any) => console.log('Post method address: ' + r),
      (error: any) => console.log('Error: ' + error.message));
  }

  newAddress() {
    this.address = new Address();
  }

}
