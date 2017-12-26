import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AddressType } from '../addressType.model';
import { AddressTypeService } from '../../../services/addressType.service';
import {Router} from "@angular/router";

@Component({
  selector: 'app-address-type-list',
  templateUrl: './address-type-list.component.html',
  styleUrls: ['./address-type-list.component.scss']
})
export class AddressTypeListComponent implements OnInit {

  public addressTypes: AddressType[];

  //@Output() editAddresTypeEvent = new EventEmitter();

  constructor(private addressTypeService: AddressTypeService, private router: Router) { }

  ngOnInit() {
    this.loadAddressTypes();
  }

  //Umjesto modala, jednostavno se isntalira i koristi
  //http://mattlewis92.github.io/angular-bootstrap-confirm/

  loadAddressTypes(): any {
    this.addressTypeService.getAddressTypes().subscribe((addressTypes: any) => {
      this.addressTypes = addressTypes;
    });
  }

  deleteAddressTypeFromLista(index: number){
    this.addressTypes.splice(index, 1);
  }
/*
  editAddressType(user: any){
this.editAddresTypeEvent.emit(user);
    /*console.log(addressType);
console.log('editAddressType uslo');
    this.router.navigate(['address/edit', addressType]);
console.log('editAddressType proslo');
console.log(addressType);
  }*/
}
