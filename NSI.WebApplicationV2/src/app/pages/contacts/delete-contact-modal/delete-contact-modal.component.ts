import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ContactsService} from '../../../services/contacts.service';

@Component({
  selector: 'app-delete-contact-modal',
  templateUrl: './delete-contact-modal.component.html',
  styleUrls: ['./delete-contact-modal.component.css']
})
export class DeleteContactModalComponent {
  @Input() contact: any;
  @Output() deleteFun = new EventEmitter();
  constructor(private contactsService: ContactsService) {
  }

  public deleteContact(id: number) {
    this.contactsService.deleteContact(id).subscribe((res: any) => {
      this.deleteFun.emit('emit');
    });
  }
}
