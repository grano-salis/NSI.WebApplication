import { Component, OnInit } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { each } from 'lodash';
import * as moment from 'moment';
import { Logger } from '../../core/services/logger.service';
declare let $: any;

const logger = new Logger('Meetings');
@Component({
  selector: 'app-meetings',
  templateUrl: './meetings.component.html',
  styleUrls: ['./meetings.component.scss']
})
export class MeetingsComponent implements OnInit {

  private _calendar: Object;

  private color: any = '#223442';
  calendarConfiguration: any = {
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
    events: []
  };

  constructor(private tasksService: TasksService) {

    this.calendarConfiguration.select = (start: any, end: any) => this._onSelect(start, end);
  }

  ngOnInit() {
    this.loadTasks();
  }

  public onCalendarReady(calendar: any): void {
    this._calendar = calendar;
    $(this._calendar).fullCalendar('option', 'contentHeight', 650);
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

  loadTasks(): any {
    this.tasksService.getTasks()
      .subscribe((r: any) => {
        const tasks = r;
        const temp: any[] = [];
        each(tasks, (task) => {
          temp.push({
            title: task.title,
            start: task.dateCreated,
            end: task.dueDate,
            color: this.color
          });
        });

        $(this._calendar).fullCalendar('renderEvents', temp, true);
      });
  }

  getData() {
    return [
      {
        title: 'All Day Event',
        start: '2017-11-01',
        color: '#223442'
      },
      {
        title: 'Long Event',
        start: '2017-11-07',
        end: '2017-12-10',
        color: '#223442'
      },
      {
        title: 'Dinner',
        start: '2017-11-14T20:00:00',
        color: '#223442'
      },
      {
        title: 'Birthday Party',
        start: '2017-11-01T07:00:00',
        color: '#223442'
      }
    ];
  }
}