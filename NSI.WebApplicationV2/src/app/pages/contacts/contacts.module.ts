import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ContactsRoutingModule } from './contacts-routing.module';
import { ContactsComponent } from './contacts.component';
import { SharedModule } from '../../shared/shared.module';
import {NewContactComponent} from './new-contact/new-contact.component';
import {AlertModule} from "ngx-bootstrap";
import {FormsModule} from "@angular/forms";

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ContactsRoutingModule,
    AlertModule,
    FormsModule
  ],
  declarations: [ContactsComponent, NewContactComponent]
})
export class ContactsModule { }
