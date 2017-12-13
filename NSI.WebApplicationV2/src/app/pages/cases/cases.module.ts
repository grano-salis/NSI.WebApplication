import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import {AlertModule} from 'ngx-bootstrap';
import {FormsModule} from '@angular/forms';
import {NewCaseComponent} from './new-case/new-case.component';
import {CasesRoutingModule} from './cases-routing.module';
import {CasesListComponent} from './cases-list/cases-list.component';
import { EditCaseComponent } from './edit-case/edit-case.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import{MeetingsModule} from "../meetings/meetings.module";


@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    CasesRoutingModule,
    AlertModule,
    FormsModule,
    MeetingsModule
  ],
  declarations: [NewCaseComponent, CasesListComponent, EditCaseComponent, DashboardComponent]
})
export class CasesModule { }
