import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ContactModalComponent} from './contact-modal/contact-modal.component';
import {ContactsRoutingModule} from './contacts-routing.module';
import {ContactsComponent} from './contacts.component';
import {SharedModule} from '../../shared/shared.module';
import {NewContactComponent} from './new-contact/new-contact.component';
import {AlertModule} from 'ngx-bootstrap';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {DeleteContactModalComponent} from './delete-contact-modal/delete-contact-modal.component';
import {ShowContactComponent} from './show-contact-modal/show-contact.component';

import { AvatarModule } from 'ng2-avatar';

import { ControlMessagesComponent } from './control-messages.component';
import { ValidationService } from './validation.service';



@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ContactsRoutingModule,
    FormsModule,
    AlertModule,
    AvatarModule.forRoot(),
    ReactiveFormsModule
  ],
  declarations: [
    ContactsComponent,
    ContactModalComponent,
    DeleteContactModalComponent,
    NewContactComponent,
    ShowContactComponent,
    ControlMessagesComponent
  ],
  providers:[ValidationService]
})
export class ContactsModule {
}
