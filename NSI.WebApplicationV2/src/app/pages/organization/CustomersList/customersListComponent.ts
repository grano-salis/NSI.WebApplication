import { Component } from '@angular/core';

@Component({
    selector:'customer-list',
    templateUrl:'./customer-list.html'
})
export class CustomersListComponent{
    
    getTen(){
        return new Array(10)
    }
}