import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import {AlertModule} from 'ngx-bootstrap';
import {FormsModule} from '@angular/forms';
import {NewCaseComponent} from './new-case/new-case.component';
import {CasesRoutingModule} from './cases-routing.module';
import {CasesListComponent} from './cases-list/cases-list.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    CasesRoutingModule,
    AlertModule,
    FormsModule
  ],
  declarations: [NewCaseComponent, CasesListComponent]
})
export class CasesModule { }
