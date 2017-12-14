import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PricingPackageListComponent } from './pricing-package-list/pricing-package-list.component';
import { PricingPackageViewComponent } from './pricing-package-view/pricing-package-view.component';
import { PricingPackageBuyComponent } from './pricing-package-buy/pricing-package-buy.component';


const routes: Routes = [
  {path:'list',component:PricingPackageListComponent},
  {path:'mypackage',component:PricingPackageViewComponent},
  {path:'buy/:packageId',component: PricingPackageBuyComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PricingPackagesRoutingModule { }
