import {Component, ElementRef, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {Contact} from './contact';
import {ContactsService} from '../../../services/contacts.service';
import {ActivatedRoute} from '@angular/router';
import {FormBuilder, FormGroup, Validators, FormArray, FormControl} from '@angular/forms';
import {ValidationService} from '../validation.service';
import {forEach} from '@angular/router/src/utils/collection';
import {objectify} from 'tslint/lib/utils';
import {AddressService} from '../../../services/address.service';


@Component({
  selector: 'new-contact-component',
  templateUrl: './new-contact-component.html',
  styleUrls: ['../contacts.component.css']
})
export class NewContactComponent {
  @Input() temp_contact: any;
  @Input() form: any;
  @Output() onClose: EventEmitter<any> = new EventEmitter();
  @ViewChild('closeBtn') closeBtn: ElementRef;
  public addresses: any[];
  phones_unique: boolean[];
  emails_unique: boolean[];

  constructor(private contactsService: ContactsService, private route: ActivatedRoute, private formBuilder: FormBuilder, private addressService: AddressService) {    // this.phones = [];
    this.temp_contact = new Contact();
    this.phones_unique = new Array();
    this.emails_unique = new Array();
    this.temp_contact.addressId = 1;
    this.fetchAddresses();
  }

  fetchAddresses(): void {
    this.addressService.getAddreses().subscribe((addresses: any) => {
      this.addresses = addresses;
    });
  }

  newContact() {
    this.temp_contact.taskId = 1;
    this.temp_contact.createdByUserId = 1;
    this.temp_contact.addressId = this.temp_contact.address.addressId;
    this.setPhonesAndEmails();
    this.contactsService.postContact(this.temp_contact).subscribe((r: any) => {

        this.temp_contact.contact1 = r.contact1;
        this.closeBtn.nativeElement.click();
        this.onClose.next(this.temp_contact);
      },
      (error: any) => console.log('Error: ', error.message));
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

  clearForm() {
  }

  deletePhone(index: number) {

    const control = <FormArray>this.form.controls['phones'];
    control.removeAt(index);
  }

  deleteEmail(index: number) {
    const control = <FormArray>this.form.controls['emails'];
    control.removeAt(index);
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

    for (let i = 0; i < this.temp_contact.phones.length; i++) {
      for (let j = i + 1; j < this.temp_contact.phones.length; j++) {
        if (this.temp_contact.phones[i].phoneNumber === this.temp_contact.phones[j].phoneNumber) {
          this.phones_unique[j] = true;
          whatToReturn = true;
        }
      }
    }
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
    for (let i = 0; i < this.temp_contact.emails.length; i++) {
      for (let j = i + 1; j < this.temp_contact.emails.length; j++) {
        if (this.temp_contact.emails[i].emailAddress === this.temp_contact.emails[j].emailAddress) {
          this.emails_unique[j] = true;
          whatToReturn = true;
        }
      }
    }
    return whatToReturn;
  }

}
