import {Component, ElementRef, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {Contact} from './contact';
import {ContactsService} from '../../../services/contacts.service';
import {ActivatedRoute} from '@angular/router';
import {FormBuilder, FormGroup, Validators, FormArray, FormControl} from '@angular/forms';
import { ValidationService } from '../validation.service';


@Component({
  selector: 'new-contact-component',
  templateUrl: './new-contact-component.html',
  styleUrls: ['../contacts.component.css']
})
export class NewContactComponent {
  form: any;
  phone: string;
  email: number;
  @Input() temp_contact: any;
  // phones: string[];
  items: any[] = [];
  objects: any[] = [];
  @Output() onClose: EventEmitter<any> = new EventEmitter();
  @ViewChild('closeBtn') closeBtn: ElementRef;

  constructor(private contactsService: ContactsService, private route: ActivatedRoute, private formBuilder: FormBuilder) {
    this.form = this.formBuilder.group({
            'firstname': ['', [Validators.required, ValidationService.lettersOnlyValidator]],
            'lastname': ['', [Validators.required, ValidationService.lettersOnlyValidator]],
            'email': ['', [Validators.required, ValidationService.emailValidator]],
            'phone': ['', [Validators.required, ValidationService.numbersOnlyValidator]],
            'items': this.formBuilder.array([ ]),
            'objects': this.formBuilder.array([])
       });
    // this.phones = [];
    this.temp_contact = new Contact();
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
    const mappedEmails = (this as any).items.value.map(( email: any) => {
      return { emailAddress: email.name };
    });
    this.temp_contact.emails = this.temp_contact.emails.concat(mappedEmails);
    this.temp_contact.phones = [{phoneNumber: this.temp_contact.phone}];
    const mappedPhones = (this as any).objects.value.map((phone: any) => {
      return { phoneNumber: phone.name };
    });
    this.temp_contact.phones = this.temp_contact.phones.concat(mappedPhones);
  }

  newPhone() {
    this.objects = this.form.get('objects');
    const formGroup = this.formBuilder.group({
      'name': ''
    });
    //formGroup.setValidators([Validators.required]);
    this.objects.push(formGroup);
    formGroup.updateValueAndValidity();
    //this.phones.push('');
  }

  newEmail() {
    this.items = this.form.get('items');
    const formGroup = this.formBuilder.group({
      'name': ''
    });
    //formGroup.setValidators([Validators.required, ValidationService.emailValidator]);
    this.items.push(formGroup);
    formGroup.updateValueAndValidity();
  }

  deletePhone(index: number) {

    const control = <FormArray>this.form.controls['objects'];
    // remove the chosen row
    control.removeAt(index);

    //this.objects.splice(index, 1);
    // this.phones.splice(index, 1);
  }

  deleteEmail(index: number) {
    const control = <FormArray>this.form.controls['items'];
    // remove the chosen row
    control.removeAt(index);
    //this.items.splice(index, 1);
  }

  trackByIndex(index: number, obj: any): any {
    return index;
  }
}
