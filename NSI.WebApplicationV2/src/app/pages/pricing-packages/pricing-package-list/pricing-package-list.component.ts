import { Component, OnInit, ViewChild } from '@angular/core';
import { PricingPackagesService } from '../../../services/pricing-packages.service';
import { SubscriptionService } from '../../../services/subscription.service';
import {ModalModule} from "ngx-modal";



@Component({
  selector: 'app-pricing-package-list',
  templateUrl: './pricing-package-list.component.html',
  styleUrls: ['./pricing-package-list.component.scss']
})
export class PricingPackageListComponent implements OnInit {

  @ViewChild('myModal') modal: any;
  pricingPackages:any[]=[];
  name:string = "names";
  bonus:number = 0;
  selectedPackage = -1;
  constructor(private pricingPackagesService: PricingPackagesService, private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.loadPricingPackages();
    //this.modal.open();
  }

  private loadPricingPackages(): void {
    this.pricingPackagesService.getPricingPackages().
    subscribe(pricingPackages => this.pricingPackages = pricingPackages);
  }

  closeModal(){
    console.log("log log log log log log log ")
  }

  subscribeToPackage(num:number){
    console.log(num);

    this.subscriptionService.getActiveSubscription(1).subscribe(
      activeSubscription =>{
        if(activeSubscription==null){

        }
        else
        {
          this.subscriptionService.getSubscriptionBonus(activeSubscription.subscriptionId,num).subscribe(
          bonus => {
              this.bonus = bonus;
              this.selectedPackage = num;
              this.modal.open();
            }
          )
      }
      }
    )
  }

}
