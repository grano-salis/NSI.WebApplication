import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DocumentCategoryComponent } from './document-category.component';
import { extract } from '../../core/services/i18n.service';

const routes: Routes = [
  { path: '', component: DocumentCategoryComponent, data: { title: extract('DocumentCategory') } },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DocumentCategoryRoutingModule { }
