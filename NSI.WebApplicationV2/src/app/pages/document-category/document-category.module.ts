import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from '../../shared/shared.module';
import {FormsModule} from '@angular/forms';
import { DocumentCategoryComponent } from './document-category.component';
import { DocumentCategoryRoutingModule } from './document-category-routing.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DocumentCategoryRoutingModule,
    FormsModule
  ],
  declarations: [DocumentCategoryComponent]
})
export class DocumentCategoryModule { }
