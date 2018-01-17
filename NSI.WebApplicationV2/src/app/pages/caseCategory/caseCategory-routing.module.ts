import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CaseCategoryComponent } from './caseCategory.component';
import { extract } from '../../core/services/i18n.service';

const routes: Routes = [
  { path: '', component: CaseCategoryComponent, data: { title: extract('CaseCategory') } },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CaseCategoryRoutingModule { }
