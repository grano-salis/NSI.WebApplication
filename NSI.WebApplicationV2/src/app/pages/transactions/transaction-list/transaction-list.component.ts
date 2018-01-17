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

  private convertUTCDateToLocalDate(date:Date) {
    var newDate = new Date(date.getTime()+date.getTimezoneOffset()*60*1000);

    var offset = date.getTimezoneOffset() / 60;
    var hours = date.getHours();

    newDate.setHours(hours - offset);

    return newDate;
  }
  
  formatTimeDate(date:string):string{
    let d = new Date(date);
    let outputDate = this.convertUTCDateToLocalDate(d);
    return String(outputDate.getDate().toString().padStart(2,'0')+"/"+(outputDate.getMonth()+1).toString().padStart(2,'0')+"/"+outputDate.getUTCFullYear().toString().padStart(4,'0'));
  }

}
