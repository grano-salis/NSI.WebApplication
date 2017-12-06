import {Component} from '@angular/core';
import {Contact} from './contact';
import {ContactsService} from '../../../services/contacts.service';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-contacts',
  templateUrl: './new-contact-component.html',
  styleUrls: []
})
export class NewContactComponent {
  model: Contact;
  contactForm: any;
  constructor(private contactsService: ContactsService, private route: ActivatedRoute) {
    this.model = new Contact();
  }

  newContact() {
    this.model.taskId = 0;
    this.model.contact1 = 100;
    this.model.createdByUserId = 1;
    this.contactsService.postContact(this.model).subscribe((r: any) => console.log('Novi kontakt: ' + r),
      (error: any) => console.log('Error: ', error.message));

  }
}
