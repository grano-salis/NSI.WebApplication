import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {ModalModule} from "ngx-modal";

import { PricingPackagesRoutingModule } from './pricing-packages-routing.module';
import { PricingPackageListComponent } from './pricing-package-list/pricing-package-list.component';
import { PricingPackageViewComponent } from './pricing-package-view/pricing-package-view.component';
import { PricingPackageBuyComponent } from './pricing-package-buy/pricing-package-buy.component';

@NgModule({
  imports: [
    CommonModule,
    PricingPackagesRoutingModule,
    FormsModule,
    ModalModule
  ],
  declarations: [PricingPackageListComponent, PricingPackageViewComponent, PricingPackageBuyComponent]
})
export class PricingPackagesModule { }
