import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { extract } from '../../core/services/i18n.service';
import { FileTypeComponent } from './file-type.component';

const routes: Routes = [
  { path: '', component: FileTypeComponent, data: { title: extract('FileType') } },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FileTypeRoutingModule { }
