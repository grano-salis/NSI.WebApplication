import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { DocumentsRoutingModule } from './documents-routing.module';
import { DocumentsComponent } from './documents.component';
import { DocumentModalComponent } from './document-modal/document-modal.component';
import { DocumentListComponent } from './document-list/document-list.component';
import { DocumentFilterListComponent } from './document-filter-list/document-filter-list.component';

import { SharedModule } from '../../shared/shared.module';
import { DocumentFilterComponent } from './document-filter-list/document-filter/document-filter.component';
import { DocumentHistoryModalComponent } from './document-history-modal/document-history-modal.component';
import { DocumentsFilterService } from '../../services/documents-filter.service';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    DocumentsRoutingModule
  ],
  declarations: [
    DocumentsComponent,
    DocumentModalComponent,
    DocumentListComponent,
    DocumentFilterListComponent,
    DocumentFilterComponent,
    DocumentHistoryModalComponent
  ],
  providers: [
    DocumentsFilterService
  ],
  entryComponents: [DocumentFilterComponent]
})
export class DocumentsModule { }
