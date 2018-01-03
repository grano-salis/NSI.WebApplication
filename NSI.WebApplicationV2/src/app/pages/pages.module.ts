import { NgModule } from '@angular/core';

import { PagesRoutingModule } from './pages-routing.module';
import { SharedModule } from '../shared/shared.module';
import {PaginationModule} from "ngx-bootstrap";

@NgModule({
  imports: [
    SharedModule,
    PagesRoutingModule,
    PaginationModule.forRoot()
  ],
  declarations: []
})
export class PagesModule { }
