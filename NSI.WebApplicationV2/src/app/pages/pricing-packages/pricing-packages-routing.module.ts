import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PricingPackageListComponent } from './pricing-package-list/pricing-package-list.component';
import { PricingPackageViewComponent } from './pricing-package-view/pricing-package-view.component';

const routes: Routes = [
  {path:'list',component:PricingPackageListComponent},
  {path:'mypackage',component:PricingPackageViewComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PricingPackagesRoutingModule { }
