import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from '../../shared/shared.module';
import {FormsModule} from '@angular/forms';
import { FileTypeComponent } from './file-type.component';
import { FileTypeRoutingModule } from './file-type-routing.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FileTypeRoutingModule,
    FormsModule
  ],
  declarations: [FileTypeComponent]
})
export class FileTypeModule { }
