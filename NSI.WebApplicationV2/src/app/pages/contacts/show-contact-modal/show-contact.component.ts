import {Component, ElementRef, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {Contact} from './contact';
import {ContactsService} from '../../../services/contacts.service';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'show-contact-component',
  templateUrl: './show-contact-component.html',
  styleUrls: []
})
export class ShowContactComponent {
  @Input() temp_contact: any;


  constructor(private contactsService: ContactsService) {
  }

}
