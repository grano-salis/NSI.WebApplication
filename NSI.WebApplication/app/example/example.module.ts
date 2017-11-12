import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { Comp1Component } from './comp1/comp1.component';
import { Comp2Component } from './comp2/comp2.component';
import { ExampleService } from './services/example.service';

const exampleRoutes: Routes = [
    { path: 'example/comp1', component: Comp1Component },
    { path: 'example/comp2/:id', component: Comp2Component },
]

@NgModule({
    imports: [CommonModule, RouterModule.forChild(exampleRoutes)],
    declarations: [Comp1Component, Comp2Component],
    providers: [ExampleService],
    exports: [Comp1Component, Comp2Component]
})
export class ExampleModule { }