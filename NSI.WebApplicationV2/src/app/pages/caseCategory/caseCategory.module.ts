import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CaseCategoryRoutingModule } from './caseCategory-routing.module';
import { SharedModule } from '../../shared/shared.module';
import {FormsModule,ReactiveFormsModule } from '@angular/forms';
import { CaseCategoryComponent } from './caseCategory.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    CaseCategoryRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [CaseCategoryComponent]
})
export class CaseCategoryModule { }
