import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MeetingsRoutingModule } from './meetings-routing.module';
import { MeetingsComponent } from './meetings.component';
import { SharedModule } from '../../shared/shared.module';
import {AlertModule} from "ngx-bootstrap";
import {FormsModule} from "@angular/forms";
import { MeetingNewComponent } from './meeting-new/meeting-new.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    MeetingsRoutingModule,
    AlertModule,
    FormsModule
  ],
  declarations: [MeetingsComponent, MeetingNewComponent]
})
export class MeetingsModule { }
