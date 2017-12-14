import { Component, OnInit } from '@angular/core';
import { PricingPackagesService } from '../../../services/pricing-packages.service';
@Component({
  selector: 'app-pricing-package-list',
  templateUrl: './pricing-package-list.component.html',
  styleUrls: ['./pricing-package-list.component.scss']
})
export class PricingPackageListComponent implements OnInit {

  pricingPackages:any[]=[];


  constructor(private pricingPackagesService: PricingPackagesService) { }

  ngOnInit() {
    this.loadPricingPackages();
  }

  private loadPricingPackages(): void {
    this.pricingPackagesService.getPricingPackages().
    subscribe(pricingPackages => this.pricingPackages = pricingPackages);
  }

}
