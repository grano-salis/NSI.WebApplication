import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { TransactionsRoutingModule } from './transactions-routing.module';
import { TransactionListComponent } from './transaction-list/transaction-list.component';

@NgModule({
  imports: [
    CommonModule,
    TransactionsRoutingModule,
    FormsModule
  ],
  declarations: [TransactionListComponent]
})
export class TransactionsModule { }
