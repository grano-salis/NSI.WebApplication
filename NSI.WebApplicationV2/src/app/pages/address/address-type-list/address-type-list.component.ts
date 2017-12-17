import { Component, OnInit } from '@angular/core';
import { AddressType } from '../addressType.model';
import { AddressTypeService } from '../../../services/addressType.service';

@Component({
  selector: 'app-address-type-list',
  templateUrl: './address-type-list.component.html',
  styleUrls: ['./address-type-list.component.scss']
})
export class AddressTypeListComponent implements OnInit {

  public addressTypes: AddressType[];

  constructor(private addressTypeService: AddressTypeService) { }

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

}
