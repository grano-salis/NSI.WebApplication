import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MeetingsRoutingModule } from './meetings-routing.module';
import { MeetingsComponent } from './meetings-overview/meetings-overview.component';
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
  declarations: [MeetingsComponent, MeetingNewComponent],
  exports: [MeetingsComponent]
})
export class MeetingsModule { }
