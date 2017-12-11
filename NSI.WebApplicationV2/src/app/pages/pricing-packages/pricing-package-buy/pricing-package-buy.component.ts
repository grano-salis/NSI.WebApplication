import { Component, OnInit,AfterViewInit } from '@angular/core';
import { TransactionsService } from '../../../services/transactions.service';
import {Router} from '@angular/router';

declare let $: any;

@Component({
  selector: 'app-pricing-package-buy',
  templateUrl: './pricing-package-buy.component.html',
  styleUrls: ['./pricing-package-buy.component.scss']
})
export class PricingPackageBuyComponent implements OnInit, AfterViewInit {

  transaction:any={Amount:77.77,PaymentGatewayId:1,PricingPackageId:2, CustomerId:1};


  constructor(private transactionsService: TransactionsService, private router: Router) { }

  ngOnInit() {
  }

  ngAfterViewInit(): void {
    $('#wizard').smartWizard();

    $('.buttonFinish').click(()=>{this.subscribeToPackage()});
  }

  subscribeToPackage(): void {
    this.transactionsService.postTransaction(this.transaction).
    subscribe(()=>{this.router.navigate(['/transactions']);});
  }

}
