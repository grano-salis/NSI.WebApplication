import {Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'appendDots'
})
export class AppendDotsPipe implements PipeTransform{
    
    transform(value: string) : string {
        return value + '...';
    }
    
}