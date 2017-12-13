import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '../../../services/subscription.service';
import { PricingPackagesService } from '../../../services/pricing-packages.service';

@Component({
  selector: 'app-pricing-package-view',
  templateUrl: './pricing-package-view.component.html',
  styleUrls: ['./pricing-package-view.component.scss']
})
export class PricingPackageViewComponent implements OnInit {

  constructor(private subscriptionService:SubscriptionService, private pricingPackagesService: PricingPackagesService) { }

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

  openCheckout(productName: string, amount: number) {
    let handler = (<any>window).StripeCheckout.configure({
        key: 'your_stripe_publishable_key',
        locale: 'auto'
    });

    handler.open({
        name: 'Our Shop',
        description: productName,
        zipCode: false,
        currency: 'gbp',
        amount: amount,
        panelLabel: "Pay 10",
        allowRememberMe: false
    });
}

  ngOnInit() {
    this.loadSubscription();
  }

}
