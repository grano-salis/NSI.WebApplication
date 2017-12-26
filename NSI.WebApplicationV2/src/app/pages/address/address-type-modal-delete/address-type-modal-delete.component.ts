import {Component, Input, Output, EventEmitter} from '@angular/core';
import {AddressTypeService} from '../../../services/addressType.service';
import {AddressType} from '../addressType.model';

@Component({
  selector: 'app-address-type-modal-delete',
  templateUrl: './address-type-modal-delete.component.html',
  styleUrls: []
})
export class AddressTypeModalDeleteComponent {

  @Input('addressType_modal') addressType_modal: AddressType;
  @Output() removeAddressTypeFromList = new EventEmitter<{id: number}>();

  constructor(private addressTypeService: AddressTypeService) {
  }

  public deleteAddressType(id: number) {
    this.addressTypeService.deleteAddressType(id).subscribe((res: any) => {
      this.removeAddressTypeFromList.emit({id: this.addressType_modal.addressTypeId});
    });
  }

}
