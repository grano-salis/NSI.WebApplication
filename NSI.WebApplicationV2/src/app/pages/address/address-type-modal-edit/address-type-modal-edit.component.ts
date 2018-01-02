import {Component, OnInit, Input, Output, EventEmitter} from '@angular/core';
import {AddressType} from '../addressType.model';
import {ActivatedRoute, Router} from '@angular/router';
import {AddressTypeService} from '../../../services/addressType.service';

@Component({
  selector: 'app-address-type-modal-edit',
  templateUrl: './address-type-modal-edit.component.html',
  styleUrls: []
})

export class AddressTypeModalEditComponent implements OnInit {

  @Input()
  addressType: AddressType;
  
  @Output() 
  editAddressTypeEvent: EventEmitter<AddressType> = new EventEmitter<AddressType>();

  addressTypeEdit: AddressType = new AddressType();

  constructor(private addressTypeService: AddressTypeService) { }

  ngOnInit() {
    Object.assign(this.addressTypeEdit, this.addressType);  
  }

  editAddressType()
  {
    this.addressTypeService.putAddressType(this.addressTypeEdit.addressTypeId, this.addressTypeEdit).subscribe((r: any) => {
      console.log('Put method address: ' + r);
      this.editAddressTypeEvent.emit(this.addressTypeEdit);
    },
    (error: any) => console.log('Error: ' + error.message));
  }
}


