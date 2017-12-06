import { Component , Input, OnInit} from '@angular/core';
import {ContactsService} from "../../../services/contacts.service";


@Component({
  selector: 'app-contact-modal',
  templateUrl: './contact-modal.component.html',
  styleUrls: []
})
export class ContactModalComponent {
  @Input() selected_contact : any;

  constructor(private contactsService: ContactsService){
  }

  ngOnInit(){

  }

  updateContact(){
    console.log(this.selected_contact);
    this.contactsService.editContact(this.selected_contact.contact1, this.selected_contact).subscribe((contact: any) => {
      this.selected_contact = contact;
    });
  }




}
