import { Component, OnInit,AfterViewInit } from '@angular/core';

declare let $: any;

@Component({
  selector: 'app-pricing-package-buy',
  templateUrl: './pricing-package-buy.component.html',
  styleUrls: ['./pricing-package-buy.component.scss']
})
export class PricingPackageBuyComponent implements OnInit, AfterViewInit {

  constructor() { }

  ngOnInit() {
  }

  ngAfterViewInit(): void {
    $('#wizard').smartWizard();
  }

}
