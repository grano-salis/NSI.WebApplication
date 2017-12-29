import {Component, OnInit, Input, Output, EventEmitter, ViewChild, ElementRef, AfterViewInit} from '@angular/core';
import * as moment from "moment";

declare var $: any;

@Component({
  selector: 'app-nsi-calendar',
  templateUrl: './nsi-calendar.component.html',
  styleUrls: ['./nsi-calendar.component.scss']
})
export class NsiCalendarComponent implements OnInit, AfterViewInit {

  @Input() events: any = [];
  @Input() nsiFullCalendarClass: string;
  @Output() onCalendarReady = new EventEmitter<any>();
  @Output() onEventClicked = new EventEmitter<any>();
  @Output() onRangeSelected = new EventEmitter<any>();
  @Input() nsiFullCalendarConfiguration: any = {
    header: {
      left: 'prev,next today',
      center: 'title',
      right: 'month,agendaWeek,agendaDay'
    },
    defaultDate: moment().format('YYYY-MM-DD'),
    selectable: true,
    selectHelper: true,
    editable: true,
    eventLimit: true,
    events: [],
    eventClick: this._eventClick(),
    select: this._selectRange(),
  };

  @ViewChild('nsiFullCalendar') public _selector: ElementRef;

  constructor() {
  }

  ngOnInit() {

    console.log(this.events);
  }

  ngAfterViewInit() {
    const calendar = $(this._selector.nativeElement).fullCalendar(this.nsiFullCalendarConfiguration);
    calendar.fullCalendar('option', 'contentHeight', 650);
    if (this.events) {
      calendar.fullCalendar('renderEvents', this.events, true);
    }
    this.onCalendarReady.emit(calendar);
  }

  private _selectRange() {
    let emmiter = this.onRangeSelected;
    return function (start: any, end: any, jsEvent: any, view: any) {
      emmiter.next({start: start, end: end});
    }
  }

  private _eventClick() {
    let emmiter = this.onEventClicked;
    return function (calEvent: any, jsEvent: any, view: any){
      emmiter.next({meetingId: calEvent.meetingId})
    }
  }
}
