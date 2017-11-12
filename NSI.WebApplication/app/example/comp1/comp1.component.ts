import { Component, HostBinding, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExampleService } from "../services/example.service";

@Component({
    selector: 'comp2-selector',
    template: `
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        F Component {{id}}
        <br/>        
        First Component
        <br/>
        Data from REST: {{dataFromRest}}
    </div>
    `
})
export class Comp1Component implements OnInit {
    constructor(private route: ActivatedRoute,
        private exampleService: ExampleService) {

    }

    public id: number;
    public dataFromRest: string;

    ngOnInit() {
        this.id = 150;
        this.exampleService.postDataToRest(this.id, "it could be from form").subscribe(x => this.dataFromRest = x);
    }
}