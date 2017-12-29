import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';

declare var $: any;

@Component({
    selector: 'my-app',
    templateUrl: `./app/app.html`
})
export class AppComponent implements OnInit {
    constructor(private router: Router, private activatedRoute: ActivatedRoute) {
    }

    ngOnInit() {
    }
}
