import {Component, ElementRef, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {Contact} from './contact';
import {ContactsService} from '../../../services/contacts.service';
import {ActivatedRoute} from '@angular/router';
import {List} from "lodash";

@Component({
  selector: 'new-contact-component',
  templateUrl: './new-contact-component.html',
  styleUrls: ['../contacts.component.css']
})
export class NewContactComponent {
  phone: number;
  email: number;
  @Input() temp_contact: any;
  @Input() phones: string[];
  @Input() emails: string[];
  @Output() onClose: EventEmitter<any> = new EventEmitter();
  @ViewChild('closeBtn') closeBtn: ElementRef;

  constructor(private contactsService: ContactsService, private route: ActivatedRoute) {
    this.phone = 0;
    this.email = 0;
    this.phones = new Array();
    this.emails = new Array();
    this.temp_contact = new Contact();
  }

  newContact() {
    this.temp_contact.taskId = 1;
    this.temp_contact.addressId = 1;
    this.temp_contact.createdByUserId = 1;
    this.contactsService.postContact(this.temp_contact).subscribe((r: any) => {
        this.temp_contact.contact1 = r.contact1;
        this.closeBtn.nativeElement.click();
        this.onClose.next(this.temp_contact);
      },
      (error: any) => console.log('Error: ', error.message));

  }

  newPhone() {
    this.phone++;
    this.phones.push(this.phone.toString());
  }

  newEmail() {
    this.email++;
    this.emails.push(this.email.toString());
  }

  deletePhone() {
    this.phone--;
    this.phones.pop();
  }

  deleteEmail() {
    this.email--;
    this.emails.pop();
  }
}
