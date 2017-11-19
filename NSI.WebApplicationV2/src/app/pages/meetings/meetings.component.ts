import { Component, OnInit } from '@angular/core';

declare let $: any;
@Component({
  selector: 'app-meetings',
  templateUrl: './meetings.component.html',
  styleUrls: ['./meetings.component.scss']
})
export class MeetingsComponent implements OnInit {

  public calendarConfiguration: any = {};
  private _calendar: Object;

  constructor() {
    this.calendarConfiguration = this.getData();
    this.calendarConfiguration.select = (start: any, end: any) => this._onSelect(start, end);
  }

  ngOnInit() {
  }

  public onCalendarReady(calendar: any): void {
    this._calendar = calendar;
  }

  private _onSelect(start: any, end: any): void {

    if (this._calendar != null) {
      const title = prompt('Event Title:');
      let eventData;
      if (title) {
        eventData = {
          title: title,
          start: start,
          end: end
        };
        $(this._calendar).fullCalendar('renderEvent', eventData, true);
      }
      $(this._calendar).fullCalendar('unselect');
    }
  }

  getData() {
    return {
      header: {
        left: 'prev,next today',
        center: 'title',
        right: 'month,agendaWeek,agendaDay'
      },
      defaultDate: '2016-03-08',
      selectable: true,
      selectHelper: true,
      editable: true,
      eventLimit: true,
      events: [
        {
          title: 'All Day Event',
          start: '2016-03-01',
          color: '#223442'
        },
        {
          title: 'Long Event',
          start: '2016-03-07',
          end: '2016-03-10',
          color: '#223442'
        },
        {
          title: 'Dinner',
          start: '2016-03-14T20:00:00',
          color: '#223442'
        },
        {
          title: 'Birthday Party',
          start: '2016-04-01T07:00:00',
          color: '#223442'
        }
      ]
    };
  }
}
