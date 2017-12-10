import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HearingsRoutingModule } from './hearings-routing.module';
import { SharedModule } from '../../shared/shared.module';
import {AlertModule} from "ngx-bootstrap";
import {FormsModule} from "@angular/forms";
import { HearingNewComponent } from './hearing-new/hearing-new.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    HearingsRoutingModule,
    AlertModule,
    FormsModule
  ],
  declarations: [HearingNewComponent]
})
export class HearingsModule { }
