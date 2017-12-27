import {AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {ContactsService} from "../../../services/contacts.service";
import {FormArray, FormBuilder, Validators} from "@angular/forms";
import {ValidationService} from "../validation.service";


@Component({
  selector: 'app-contact-modal',
  templateUrl: './contact-modal.component.html',
  styleUrls: ['../contacts.component.css']
})
export class ContactModalComponent implements OnInit, AfterViewInit {
  @Input() temp_contact: any;
  @Input() form: any;
  @Output() onClose: EventEmitter<any> = new EventEmitter();
  @ViewChild('closeBtn') closeBtn: ElementRef;
  phones_unique: boolean[];
  emails_unique: boolean[];

  constructor(private contactsService: ContactsService, private formBuilder: FormBuilder) {
    this.phones_unique = new Array();
    this.emails_unique = new Array();
  }

  ngAfterViewInit() {
  }

  ngOnInit() {
  }

  updateContact() {
    this.setPhonesAndEmails();
    this.contactsService.editContact(this.temp_contact).subscribe((contact: any) => {
      this.closeBtn.nativeElement.click();
      this.onClose.next(this.temp_contact); // emit event
    });
  }

  newPhone() {
    const emails = this.form.get('phones');
    const formGroup = this.formBuilder.group({
        'name': ['', [Validators.required, ValidationService.numbersOnlyValidator]]
      })
    ;
    emails.push(formGroup);
  }

  newEmail() {
    const emails = this.form.get('emails');
    const formGroup = this.formBuilder.group({
        'name': ['', [Validators.required, ValidationService.emailValidator]],
      })
    ;
    emails.push(formGroup);
  }

  setPhonesAndEmails(): void {
    const emails = this.form.get('emails');
    this.temp_contact.emails = [{emailAddress: this.temp_contact.email}];
    const mappedEmails = emails.value.map((email: any) => {
      return {emailAddress: email.name};
    });
    const phones = this.form.get('phones');
    this.temp_contact.emails = this.temp_contact.emails.concat(mappedEmails);
    this.temp_contact.phones = [{phoneNumber: this.temp_contact.phone}];
    const mappedPhones = phones.value.map((phone: any) => {
      return {phoneNumber: phone.name};
    });
    this.temp_contact.phones = this.temp_contact.phones.concat(mappedPhones);
  }

  onChangePhone(index: number): any {
    const phones = this.form.get('phones');
    this.temp_contact.phones = [{phoneNumber: this.temp_contact.phone}];
    const mappedPhones = phones.value.map((phone: any) => {
      return {phoneNumber: phone.name};
    });
    let whatToReturn = false;
    this.temp_contact.phones = this.temp_contact.phones.concat(mappedPhones);
    for (var i = 0; i < this.temp_contact.phones.length; i++) this.phones_unique[i] = false;

    console.log('phones', this.temp_contact.phones);
    for (let i = 0; i < this.temp_contact.phones.length; i++) {
      for (let j = i + 1; j < this.temp_contact.phones.length; j++) {
        if (this.temp_contact.phones[i].phoneNumber === this.temp_contact.phones[j].phoneNumber) {
          this.phones_unique[j] = true;
          whatToReturn = true;
        }
      }
    }
    console.log('index', index);
    return whatToReturn;
  }

  onChangeEmail(index: number): any {
    const emails = this.form.get('emails');
    this.temp_contact.emails = [{emailAddress: this.temp_contact.email}];
    const mappedEmails = emails.value.map((email: any) => {
      return {emailAddress: email.name};
    });
    let whatToReturn = false;
    this.temp_contact.emails = this.temp_contact.emails.concat(mappedEmails);
    for (var i = 0; i < this.temp_contact.emails.length; i++) this.emails_unique[i] = false;
    console.log('emails', this.temp_contact.emails);
    for (let i = 0; i < this.temp_contact.emails.length; i++) {
      for (let j = i + 1; j < this.temp_contact.emails.length; j++) {
        if (this.temp_contact.emails[i].emailAddress === this.temp_contact.emails[j].emailAddress) {
          this.emails_unique[j] = true;
          whatToReturn = true;
        }
      }
    }
    console.log('index', index);
    return whatToReturn;
  }
  deletePhone(index: number) {

    const control = <FormArray>this.form.controls['phones'];
    control.removeAt(index);
  }

  deleteEmail(index: number) {
    const control = <FormArray>this.form.controls['emails'];
    control.removeAt(index);
  }


}
