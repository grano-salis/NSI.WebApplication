import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactModalComponent } from './contact-modal/contact-modal.component';
import { ContactsRoutingModule } from './contacts-routing.module';
import { ContactsComponent } from './contacts.component';
import { SharedModule } from '../../shared/shared.module';
import {DeleteContactModalComponent} from './delete-contact-modal/delete-contact-modal.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ContactsRoutingModule
  ],
  declarations: [
    ContactsComponent,
    ContactModalComponent,
  DeleteContactModalComponent]
})
export class ContactsModule { }
