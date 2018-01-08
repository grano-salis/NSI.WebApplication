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
import {ContactsComponent} from "../contacts/contacts.component";
import {ContactsModule} from "../contacts/contacts.module";
import { CaseDetailComponent } from './case-detail/case-detail.component';
import { DocumentsModule } from '../documents/documents.module';
import {DocumentsComponent} from "../documents/documents.component";



@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    CasesRoutingModule,
    AlertModule,
    FormsModule,
    MeetingsModule,
    ContactsModule,
    DocumentsModule,
  ],
  declarations: [NewCaseComponent, CasesListComponent, EditCaseComponent, DashboardComponent, CaseDetailComponent]
})
export class CasesModule { }
