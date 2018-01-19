import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from '../../shared/shared.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { FileTypeComponent } from './file-type.component';
import { FileTypeRoutingModule } from './file-type-routing.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FileTypeRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [FileTypeComponent]
})
export class FileTypeModule { }
