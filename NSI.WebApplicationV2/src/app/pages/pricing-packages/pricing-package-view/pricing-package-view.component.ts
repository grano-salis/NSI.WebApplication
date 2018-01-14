import { Component, OnInit,AfterViewInit } from '@angular/core';
import { SubscriptionService } from '../../../services/subscription.service';
import { PricingPackagesService } from '../../../services/pricing-packages.service';
import { Router } from '@angular/router';

import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../../../environments/environment';

declare let $: any;
declare let paypal:any;
declare let braintree:any;

@Component({
  selector: 'app-pricing-package-view',
  templateUrl: './pricing-package-view.component.html',
  styleUrls: ['./pricing-package-view.component.scss']
})
export class PricingPackageViewComponent implements OnInit {

  constructor(private subscriptionService:SubscriptionService, private pricingPackagesService: PricingPackagesService,private http:HttpClient, private router: Router) { }

  subscription:any=undefined;
  pricingPackage:any=null;
  packageId:number=0;


  private loadSubscription():void{
    this.subscriptionService.getActiveSubscription(1).
    subscribe(subscriptionAnswer => {
      console.log(subscriptionAnswer);
      this.subscription = subscriptionAnswer;
      this.pricingPackagesService.getPricingPackageById(this.subscription.pricingPackageId).
      subscribe(pricingPackage => {
        this.pricingPackage = pricingPackage
        this.packageId = pricingPackage.pricingPackageId;
        $('#stripe-checkout-button').click(()=>{this.extendPackageSubscriptionStripe()});
        this.loadToken(this.pricingPackage.price,this.packageId);
      })
    })
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

  ngOnInit() {
    this.loadSubscription();
  }

  ngAfterViewInit():void{

  }

  //Stripe payment logic - END

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


        this.http.post(environment.serverUrl +'/api/Transactions/MakeExtendSubscriptionPaymentStripe', bodyString, {headers:headers}).
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


    extendPackageSubscriptionStripe(){
        this.openCheckout("Paket"+this.pricingPackage.pricingPackageName,this.pricingPackage.price*1.05*100,(token: any) => this.takePayment("Paket"+this.pricingPackage.pricingPackageName,this.pricingPackage.price*1.05*100, token));
    }

    //Stripe payment logic - END


    //Paypal button creation logic - START

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
                      url: environment.serverUrl +'/api/Transactions/MakeExtendSubscriptionPaymentPaypal',
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

    //Paypal button creation logic - END


}
