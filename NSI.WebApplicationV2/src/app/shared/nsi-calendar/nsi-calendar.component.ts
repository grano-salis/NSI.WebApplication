import { Component, OnInit, Input, Output, EventEmitter, ViewChild, ElementRef, AfterViewInit } from '@angular/core';

declare var $: any;

@Component({
  selector: 'app-nsi-calendar',
  templateUrl: './nsi-calendar.component.html',
  styleUrls: ['./nsi-calendar.component.scss']
})
export class NsiCalendarComponent implements OnInit, AfterViewInit {

  @Input() nsiFullCalendarConfiguration: Object;
  @Input() nsiFullCalendarClass: string;
  @Output() onCalendarReady = new EventEmitter<any>();

  @ViewChild('nsiFullCalendar') public _selector: ElementRef;
  constructor() { }

  ngOnInit() {

  }

  ngAfterViewInit() {
    const calendar = $(this._selector.nativeElement).fullCalendar(this.nsiFullCalendarConfiguration);
    this.onCalendarReady.emit(calendar);
  }

}
