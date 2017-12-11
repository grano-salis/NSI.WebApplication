import { Component, OnInit,AfterViewInit,NgModule} from '@angular/core';
import { TransactionsService } from '../../../services/transactions.service';
import { PricingPackagesService } from '../../../services/pricing-packages.service';
import { Router, ActivatedRoute } from '@angular/router';

declare let $: any;

@Component({
  selector: 'app-pricing-package-buy',
  templateUrl: './pricing-package-buy.component.html',
  styleUrls: ['./pricing-package-buy.component.scss']
})
export class PricingPackageBuyComponent implements OnInit, AfterViewInit {

  packageId:number=0;
  transaction:any={Amount:0,PaymentGatewayId:1,PricingPackageId:0, CustomerId:1};
  pricingPackageLoaded: boolean = false;


  constructor(private transactionsService: TransactionsService, private pricingPackagesService:PricingPackagesService,  private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {

  }

  ngAfterViewInit(): void {
    $('#wizard').smartWizard();

    $('.buttonFinish').click(()=>{this.subscribeToPackage()});

    this.packageId= +this.route.snapshot.paramMap.get('packageId');
    this.loadPricingPackage(this.packageId);
  }

  loadPricingPackage(pricingPackageId:number){
    this.pricingPackagesService.getPricingPackageById(pricingPackageId).
    subscribe(pricingPackage => {
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

}
