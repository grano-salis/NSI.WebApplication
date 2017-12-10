import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { extract } from '../../core/services/i18n.service';
import {NewCaseComponent} from './new-case/new-case.component';
import {CasesListComponent} from './cases-list/cases-list.component';

const routes: Routes = [
  { path: 'new', component: NewCaseComponent},
  { path: 'all', component: CasesListComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  declarations: []
})
export class CasesRoutingModule { }
