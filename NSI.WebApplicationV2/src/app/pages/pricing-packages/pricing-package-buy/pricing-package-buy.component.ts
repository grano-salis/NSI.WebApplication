import { Component, OnInit,AfterViewInit,NgModule} from '@angular/core';
import { TransactionsService } from '../../../services/transactions.service';
import { PricingPackagesService } from '../../../services/pricing-packages.service';
import { Router, ActivatedRoute } from '@angular/router';

import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../../../environments/environment';

declare let $: any;

@Component({
  selector: 'app-pricing-package-buy',
  templateUrl: './pricing-package-buy.component.html',
  styleUrls: ['./pricing-package-buy.component.scss']
})
export class PricingPackageBuyComponent implements OnInit, AfterViewInit {

  packageId:number=0;
  pricingPackage:any;
  transaction:any={Amount:0,PaymentGatewayId:1,PricingPackageId:0, CustomerId:1};
  pricingPackageLoaded: boolean = false;


  constructor(private transactionsService: TransactionsService, private pricingPackagesService:PricingPackagesService,  private router: Router, private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit() {

  }

  ngAfterViewInit(): void {
    $('#wizard').smartWizard();

    $('.buttonFinish').click(()=>{this.buyPackage()});

    this.packageId= +this.route.snapshot.paramMap.get('packageId');
    this.loadPricingPackage(this.packageId);
  }

  loadPricingPackage(pricingPackageId:number){
    this.pricingPackagesService.getPricingPackageById(pricingPackageId).
    subscribe(pricingPackage => {
      this.pricingPackage = pricingPackage;
      this.transaction.Amount = pricingPackage.price;
      this.transaction.PricingPackageId = pricingPackage.pricingPackageId;

      this.pricingPackageLoaded=true;

      console.log(this.transaction.Amount+"   "+this.transaction.PricingPackageId);
    })
  }

  subscribeToPackage(): void {
    if(this.pricingPackageLoaded==true){
      this.transactionsService.postTransaction(this.transaction).
      subscribe(()=>{this.router.navigate(['/transactions']);});
    }
  }


    takePaymentResult: string;

    takePayment(productName: string, amount: number, token: any) {
        let body = {
            tokenId: token.id,
            productName: productName,
            amount: amount,
            packageId: this.packageId
        };
        let bodyString = JSON.stringify(body);
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });


        this.http.post(environment.serverUrl +'/api/Transactions/MakePayment', bodyString, {headers:headers}).
        subscribe((res:any) => {
            this.takePaymentResult = res.status;
            this.router.navigate(['/transactions']);
            },
            (error) => {
                this.takePaymentResult = error.message
            }
          );


    }

    openCheckout(productName: string, amount: number, tokenCallback:any) {
        let handler = (<any>window).StripeCheckout.configure({
            key: 'pk_test_DtZhhM6VxNCPaiYjPdYTQQaY',
            locale: 'auto',
            token: tokenCallback
        });

        handler.open({
            name: 'Our Shop',
            description: productName,
            zipCode: false,
            currency: 'usd',
            amount: amount,
            panelLabel: "Pay {{amount}}",
            allowRememberMe: false
        });
    }

    buyTShirt() {
        this.openCheckout("T-Shirt", 1000, (token: any) => this.takePayment("T-Shirt", 1000, token));
    }
    buyTrainers() {
        this.openCheckout("Trainers", 1500, (token: any) => this.takePayment("Trainers", 1500, token));
    }
    buyJeans() {
        this.openCheckout("Jeans", 2002, (token: any) => this.takePayment("Jeans", 2002, token));
    }

    buyPackage(){
        this.openCheckout("Paket"+this.pricingPackage.pricingPackageName,this.transaction.Amount*1.05*100,(token: any) => this.takePayment("Paket"+this.pricingPackage.pricingPackageName,this.transaction.Amount*1.05*100, token));
    }

}
