import { Component, OnInit } from '@angular/core';
import {Hearing} from '../../hearings/hearing-new/hearing';
import {Address} from '../address.model';

@Component({
  selector: 'app-address-new',
  templateUrl: './address-new.component.html',
  styleUrls: ['./address-new.component.scss']
})
export class AddressNewComponent implements OnInit {

  model: Address;

  constructor() {
    this.model = new Address();
  }

  ngOnInit() {
  }

  onSubmit() {
    console.log('Usao');
  }

  newAddress() {
    this.model = new Address();
  }

}
