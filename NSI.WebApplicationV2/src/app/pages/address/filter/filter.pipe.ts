import { Pipe, PipeTransform } from '@angular/core';
import { Address } from '../address.model';

@Pipe({
    name: 'filter'
})

export class FilterPipe implements PipeTransform {

    transform(items: Address[], searchText: string): any[] {
        if (!items) return [];
        if (!searchText) return items;
        searchText = searchText.toLowerCase();
        return items.filter(item => {
            return (item.address1.toString().toLowerCase().includes(searchText) || item.city.toString().toLowerCase().includes(searchText));
        });
    }
}