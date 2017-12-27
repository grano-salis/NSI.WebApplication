import {Component, OnInit, Input, Output, EventEmitter} from '@angular/core';
import {AddressType} from '../addressType.model';
import {ActivatedRoute, Router} from '@angular/router';
import {AddressTypeService} from '../../../services/addressType.service';

@Component({
  selector: 'app-address-type-modal-edit',
  templateUrl: './address-type-modal-edit.component.html',
  styleUrls: []
})
export class AddressTypeModalEditComponent {

  @Input() addressType: AddressType;
  @Output() editAddresTypeEvent = new EventEmitter();
  addressTypeEdit: AddressType= new AddressType();

  constructor(private addressTypeService: AddressTypeService) { }

  ngOnInit() {
    Object.assign(this.addressTypeEdit, this.addressType);
  }
  editAddressType(){
    console.log('uslo');
    console.log(this.addressType);
    this.editAddresTypeEvent.emit({orginal: this.addressType, edited: this.addressTypeEdit})
    console.log('uslo1');
    this.addressTypeService.putAddressType(this.addressTypeEdit.addressTypeId, this.addressTypeEdit);
    console.log('uslo2');
  }
}


