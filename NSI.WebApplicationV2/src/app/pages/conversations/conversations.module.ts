import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConversationsComponent } from './conversations.component';
import { SharedModule } from '../../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { ConversationsRoutingModule } from "./conversations-routing.module";
import { AngularMultiSelectModule} from 'angular2-multiselect-dropdown/angular2-multiselect-dropdown';
import {ToolTipModule} from 'angular2-tooltip';
import { AppendDotsPipe } from './append-dots.pipe';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    ConversationsRoutingModule,
    AngularMultiSelectModule,
    ToolTipModule
  ],
  declarations: [ConversationsComponent, AppendDotsPipe]
})
export class ConversationsModule { }
