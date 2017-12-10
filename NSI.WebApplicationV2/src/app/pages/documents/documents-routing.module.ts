import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DocumentsComponent } from './documents.component';
import { extract } from '../../core/services/i18n.service';

const routes: Routes = [
  { path: '', component: DocumentsComponent, data: { title: extract('Documents') } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class DocumentsRoutingModule { }
