import { Component, OnInit } from '@angular/core';
import { TransactionsService } from '../../../services/transactions.service';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.scss']
})
export class TransactionListComponent implements OnInit {

  transakcije = [{iznos:'1',datum:'d',paket:'paket',placanje:"DA",status:"STATUS"},{iznos:'1',datum:'d',paket:'paket',placanje:"DA",status:"STATUS"},{iznos:'1',datum:'d',paket:'paket',placanje:"DA",status:"STATUS"},{iznos:'1',datum:'d',paket:'paket',placanje:"DA",status:"STATUS"}]

  transactions:any[]=[];

  constructor(private transactionsService:TransactionsService ) { }

  private loadTransactions():void{
    this.transactionsService.getTransactions().
    subscribe(transactions => this.transactions = transactions);
  }

  ngOnInit() {
    this.loadTransactions();
  }

}
