import { Component, HostBinding, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExampleService } from "../services/example.service";

@Component({
    selector: 'comp2-selector',
    template: `
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        Other {{id}}
        <br/>        
        Some content
        <br/>
        Data from REST: {{dataFromRest}}
    </div>
    `
})
export class Comp2Component implements OnInit {
    constructor(private route: ActivatedRoute,
        private exampleService: ExampleService) {

    }

    public id: Number;
    public dataFromRest: string;

    ngOnInit() {
        this.id = +this.route.snapshot.params['id'];
        this.exampleService.getDataFromRest().subscribe(x => this.dataFromRest = x);
    }
}