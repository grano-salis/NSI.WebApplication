import {Component, ElementRef, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {Contact} from './contact';
import {ContactsService} from '../../../services/contacts.service';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'new-contact-component',
  templateUrl: './new-contact-component.html',
  styleUrls: []
})
export class NewContactComponent {
  @Input() temp_contact: any;
  @Output() onClose: EventEmitter<any> = new EventEmitter();
  @ViewChild('closeBtn') closeBtn: ElementRef;
  model: Contact;

  constructor(private contactsService: ContactsService, private route: ActivatedRoute) {
    this.model = new Contact();
  }

  newContact() {
    this.model.taskId = 0;
    this.model.addressId = 1;
    this.model.createdByUserId = 1;
    this.contactsService.postContact(this.model).subscribe((r: any) => {
        console.log('Novi kontakt: ' + r)
        this.closeBtn.nativeElement.click();
        this.temp_contact = this.model;
        this.onClose.next(null);
      },
      (error: any) => console.log('Error: ', error.message));

  }
}
