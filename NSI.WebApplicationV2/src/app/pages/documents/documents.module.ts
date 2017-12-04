import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DocumentsRoutingModule } from './documents-routing.module';
import { DocumentsComponent } from './documents.component';
import { DocumentNewComponent } from './document-new/document-new.component';
import { DocumentModalComponent } from './document-modal/document-modal.component';
import { DocumentListComponent } from './document-list/document-list.component';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DocumentsRoutingModule
  ],
  declarations: [
    DocumentsComponent,
    DocumentNewComponent,
    DocumentModalComponent,
    DocumentListComponent
  ]
})
export class DocumentsModule { }
