import { Component, OnInit,AfterViewInit,NgModule} from '@angular/core';
import { TransactionsService } from '../../../services/transactions.service';
import { PricingPackagesService } from '../../../services/pricing-packages.service';
import { Router, ActivatedRoute } from '@angular/router';

import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../../../environments/environment';

declare let $: any;
declare let paypal:any;
declare let braintree:any;

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
  selfref:any;


  constructor(private transactionsService: TransactionsService, private pricingPackagesService:PricingPackagesService,  private router: Router, private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit() {

  }

  ngAfterViewInit(): void {
    $('#wizard').smartWizard();

    $('#stripe-checkout-button').click(()=>{this.buyPackage()});

    $('.buttonFinish').hide();

    this.packageId= +this.route.snapshot.paramMap.get('packageId');
    this.loadPricingPackage(this.packageId);
    this.selfref = this;
  }

  loadPricingPackage(pricingPackageId:number){
    this.pricingPackagesService.getPricingPackageById(pricingPackageId).
    subscribe(pricingPackage => {
      this.pricingPackage = pricingPackage;
      this.transaction.Amount = pricingPackage.price;
      this.transaction.PricingPackageId = pricingPackage.pricingPackageId;

      this.pricingPackageLoaded=true;

      console.log(this.transaction.Amount+"   "+this.transaction.PricingPackageId);
      this.loadToken(this.transaction.Amount,this.packageId);
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

    loadToken(tAmount:number,tPricingPackageId:number){
      this.http.get(environment.serverUrl +'/api/Transactions/PaypalToken').
      subscribe((res:any)=>{

        paypal.Button.render({

            // Pass in the Braintree SDK

            braintree: braintree,

            // Pass in your Braintree authorization key

            client: {
                sandbox: res,
                production: '<insert production auth key>'
            },

            // Set your environment

            env: 'sandbox', // sandbox | production

            // Wait for the PayPal button to be clicked

            payment: function(data:any, actions:any) {

                // Make a call to create the payment

                return actions.payment.create({
                    payment: {
                        transactions: [
                            {
                                amount: { total: tAmount*1.05, currency: 'USD' }
                            }
                        ]
                    }
                });
            },

            // Wait for the payment to be authorized by the customer

            onAuthorize: (data:any, actions:any)=>{
              console.log(tPricingPackageId+" package idddddd")
                return actions.payment.tokenize().then((data:any) =>{
                    console.log('Braintree nonce:', data.nonce);
                    $.ajax({
                      type: "POST",
                      url: environment.serverUrl +'/api/Transactions/CreatePaypalTransaction',
                      data: JSON.stringify({ paymentNonce: data.nonce, amount:tAmount*1.05, packageId:tPricingPackageId }),
                      dataType: "json",
                      contentType: 'application/json'
                    }).done((data:any)=>{
                      this.router.navigate(['/transactions']);
                    });

                });
            }

        }, '#paypal-button-container');


      });
    }

}
