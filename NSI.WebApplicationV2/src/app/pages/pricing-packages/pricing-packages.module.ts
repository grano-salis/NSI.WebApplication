import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PricingPackagesRoutingModule } from './pricing-packages-routing.module';
import { PricingPackageListComponent } from './pricing-package-list/pricing-package-list.component';
import { PricingPackageViewComponent } from './pricing-package-view/pricing-package-view.component';

@NgModule({
  imports: [
    CommonModule,
    PricingPackagesRoutingModule
  ],
  declarations: [PricingPackageListComponent, PricingPackageViewComponent]
})
export class PricingPackagesModule { }
