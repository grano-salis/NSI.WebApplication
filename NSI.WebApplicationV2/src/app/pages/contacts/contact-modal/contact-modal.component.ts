import {AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {ContactsService} from "../../../services/contacts.service";


@Component({
  selector: 'app-contact-modal',
  templateUrl: './contact-modal.component.html',
  styleUrls: ['../contacts.component.css']
})
export class ContactModalComponent implements OnInit, AfterViewInit {
  @Input() temp_contact: any;
  @Output() onClose: EventEmitter<any> = new EventEmitter();
  @ViewChild('closeBtn') closeBtn: ElementRef;
  constructor(private contactsService: ContactsService) {

  }

  ngAfterViewInit() {
  }

  ngOnInit() {

  }

  updateContact() {
    this.contactsService.editContact(this.temp_contact).subscribe((contact: any) => {
      this.closeBtn.nativeElement.click();
      this.onClose.next(this.temp_contact); // emit event
    });
  }


}
