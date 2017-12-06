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
  constructor(private contactsService: ContactsService, private route: ActivatedRoute) {
    this.model = new Contact();
  }

}
