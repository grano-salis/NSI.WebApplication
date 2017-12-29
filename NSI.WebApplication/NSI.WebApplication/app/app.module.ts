import { NgModule }                 from '@angular/core';
import { BrowserModule }            from '@angular/platform-browser';
import { FormsModule }              from '@angular/forms';
import { RouterModule }             from '@angular/router';

import { SharedModule } from './shared/shared.module';

import { ExampleModule } from './example/example.module';

import { AppComponent }             from './app.component';

@NgModule({
    imports: [/*CoreModule.forRoot(),*/ SharedModule, BrowserModule, FormsModule, ExampleModule, RouterModule.forRoot([])],
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }
