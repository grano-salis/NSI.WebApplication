import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '../../../services/subscription.service';
import { PricingPackagesService } from '../../../services/pricing-packages.service';

import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-pricing-package-view',
  templateUrl: './pricing-package-view.component.html',
  styleUrls: ['./pricing-package-view.component.scss']
})
export class PricingPackageViewComponent implements OnInit {

  constructor(private subscriptionService:SubscriptionService, private pricingPackagesService: PricingPackagesService,private http:HttpClient) { }

  subscription:any=null;
  pricingPackage:any=null;

  private loadSubscription():void{
    this.subscriptionService.getActiveSubscription(1).
    subscribe(subscriptionAnswer => {
      this.subscription = subscriptionAnswer;
      this.pricingPackagesService.getPricingPackageById(this.subscription.pricingPackageId).
      subscribe(pricingPackage => this.pricingPackage = pricingPackage)
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

  saveChanges(): void{
      console.log("sdasdasda");
    console.log(this.subscription);
    this.subscriptionService.updateSubscription(this.subscription).subscribe(
    subscriptionAnswer => {
      this.subscription = subscriptionAnswer;
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

  takePayment(productName: string, amount: number, token: any) {
      let body = {
          tokenId: token.id,
          productName: productName,
          amount: amount,
          packageId: this.pricingPackage.pricingPackageId
      };
      let bodyString = JSON.stringify(body);
      let headers = new HttpHeaders({ 'Content-Type': 'application/json' });


      this.http.post(environment.serverUrl +'/api/Transactions/MakePayment', bodyString, {headers:headers}).
      subscribe((res:any) => {
          this.loadSubscription();
          },
          (error) => {
              console.log(error);
          }
        );
  }

  buyPackage(){
      this.openCheckout("Paket"+this.pricingPackage.pricingPackageName,this.pricingPackage.Price*1.05*100,(token: any) => this.takePayment("Paket"+this.pricingPackage.pricingPackageName,this.pricingPackage.Price*1.05*100, token));
  }


}
