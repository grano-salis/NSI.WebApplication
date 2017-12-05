import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConversationsComponent } from './conversations.component';
import { SharedModule } from '../../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { ConversationsRoutingModule } from "./conversations-routing.module";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    ConversationsRoutingModule
  ],
  declarations: [ConversationsComponent]
})
export class ConversationsModule { }
