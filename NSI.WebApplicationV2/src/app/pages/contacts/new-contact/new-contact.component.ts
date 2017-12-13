import {Component, ElementRef, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {Contact} from './contact';
import {ContactsService} from '../../../services/contacts.service';
import {ActivatedRoute} from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ValidationService } from '../validation.service';
import {validate} from "codelyzer/walkerFactory/walkerFn";
import {forEach} from "@angular/router/src/utils/collection";
import {isBoolean} from "util";

@Component({
  selector: 'new-contact-component',
  templateUrl: './new-contact-component.html',
  styleUrls: ['../contacts.component.css']
})
export class NewContactComponent {
  form: any;
  phone: number;
  email: number;
  @Input() temp_contact: any;
  phones: string[];
  emails: string[];
  @Output() onClose: EventEmitter<any> = new EventEmitter();
  @ViewChild('closeBtn') closeBtn: ElementRef;

  constructor(private contactsService: ContactsService, private route: ActivatedRoute, private formBuilder: FormBuilder) {
    this.form = this.formBuilder.group({
      'firstname': ['', [Validators.required, ValidationService.lettersOnlyValidator]],
      'lastname': ['', [Validators.required, ValidationService.lettersOnlyValidator]],
      'email':['', [Validators.required, ValidationService.emailValidator]],
      'phone':['', [Validators.required, ValidationService.numbersOnlyValidator]],
      'emails[index]':  ['', [Validators.required]],
      'phones[index]':  ['', [Validators.required]]
    });

    console.log(this.form);
    this.phones = [];
    this.emails = [];
    this.temp_contact = new Contact();
  }

  validateIfDuplicated (list:string[])
  {
    let i : number = 0;
    let j : number = 0;
    for (i=0; i<list.length; i++){
      for (j=0; j<list.length; j++){
        if (list[i] == list[j]) return true;
      }
    }
    return false;
  }


  newContact() {
    this.temp_contact.taskId = 1;
    this.temp_contact.addressId = 1;
    this.temp_contact.createdByUserId = 1;
    this.setPhonesAndEmails();
    this.contactsService.postContact(this.temp_contact).subscribe((r: any) => {
        this.temp_contact.contact1 = r.contact1;
        this.closeBtn.nativeElement.click();
        this.onClose.next(this.temp_contact);
      },
      (error: any) => console.log('Error: ', error.message));
  }

  setPhonesAndEmails(): void {
    this.temp_contact.emails = [{emailAddress: this.temp_contact.email}];
    const mappedEmails = this.emails.map((email: string) => {
      return { emailAddress: email };
    });
    this.temp_contact.emails = this.temp_contact.emails.concat(mappedEmails);
    this.temp_contact.phones = [{phoneNumber: this.temp_contact.phone}];
    const mappedPhones = this.phones.map((phone: string) => {
      return { phoneNumber: phone };
    });
    this.temp_contact.phones = this.temp_contact.phones.concat(mappedPhones);
  }

  newPhone() {
    this.phones.push('');
  }

  newEmail() {
    this.emails.push('');
  }

  deletePhone() {
    this.phones.pop();
  }

  deleteEmail() {
    this.emails.pop();
  }

  trackByIndex(index: number, obj: any): any {
    return index;
  }
}
