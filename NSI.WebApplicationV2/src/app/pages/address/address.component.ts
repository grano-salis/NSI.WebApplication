import { Component, OnInit } from '@angular/core';
import {Address} from './address.model';
import {AddressService} from '../../services/address.service';
import {Observable} from 'rxjs/Observable';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: []
})
export class AddressComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
