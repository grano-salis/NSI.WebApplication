import {AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {ContactsService} from "../../../services/contacts.service";
import {FormArray, FormBuilder, Validators} from "@angular/forms";
import {ValidationService} from "../validation.service";
import {AddressService} from "../../../services/address.service";


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
  public addresses: any[];

  constructor(private contactsService: ContactsService, private formBuilder: FormBuilder,  private addressService: AddressService) {
    this.fetchAddresses();
  }

  ngAfterViewInit() {
  }

  ngOnInit() {
  }

  fetchAddresses(): void {
    this.addressService.getAddreses().subscribe((addresses: any) => {
      this.addresses = addresses;
    });
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

  deletePhone(index: number) {

    const control = <FormArray>this.form.controls['phones'];
    control.removeAt(index);
  }

  deleteEmail(index: number) {
    const control = <FormArray>this.form.controls['emails'];
    control.removeAt(index);
  }


}
