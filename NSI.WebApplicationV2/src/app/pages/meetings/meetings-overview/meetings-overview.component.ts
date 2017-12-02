import {Component, OnInit} from '@angular/core';
import {TasksService} from '../../../services/tasks.service';
import {each} from 'lodash';
import * as moment from 'moment';
import {Logger} from '../../../core/services/logger.service';
import {HelperService} from "../../../services/helper.service";
import {MeetingsService} from "../../../services/meetings.service";

declare let $: any;

const logger = new Logger('Meetings');

@Component({
  selector: 'app-meetings',
  templateUrl: './meetings-overview.component.html',
  styleUrls: ['./meetings-overview.component.scss']
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

  dateSelected: boolean;

  eventModel: any = {
    dateFrom: null,
    dateTo: null,
    title: '',
    desc: ''
  };

  formSubmitted: boolean;


  constructor(private meetingsService: MeetingsService) {

    this.calendarConfiguration.select = (start: any, end: any) => this._onSelect(start, end);
  }


  // ****************
  // Public methods
  // ****************
  ngOnInit() {
    this.loadMeetings();
  }

  submitEvent(form: any) {

    this.formSubmitted = true;
    $(this._calendar).fullCalendar('unselect');
    if (form.invalid) {
      return;
    }
    let eventData = {
      start: this.eventModel.dateFrom,
      end: this.eventModel.dateTo,
      title: this.eventModel.title
    };

    $(this._calendar).fullCalendar('renderEvent', eventData, true);
    this.dateSelected = false;
    this.formSubmitted = false;
    $(this._calendar).fullCalendar('unselect');
    //todo: add service
  }

  cancelNewEvent() {
    this.eventModel = {
      dateFrom: null,
      dateTo: null,
      title: '',
      desc: ''
    };
    this.dateSelected = false;
  }

  onCalendarReady(calendar: any): void {
    this._calendar = calendar;
    $(this._calendar).fullCalendar('option', 'contentHeight', 650);
  }

  // ****************
  // Private methods
  // ****************
  private _onSelect(start: any, end: any): void {

    if (this._calendar != null) {

      this.dateSelected = true;

      HelperService.scrollToTop();

      this.eventModel = {
        dateFrom: start,
        dateTo: end
      };


      // if (title) {
      //   eventData = {
      //     title: title,
      //     start: start,
      //     end: end
      //   };
      //   $(this._calendar).fullCalendar('renderEvent', eventData, true);
      // }

    }
  }

  private loadMeetings(): any {
    this.meetingsService.getMeetings()
      .subscribe((r: any) => {
        const temp: any[] = [];
        each(r, (item) => {
          temp.push({
            title: 'Meeting ID: ' + item.meetingId,
            start: item.from,
            end: item.to,
            color: this.color
          });
        });

        $(this._calendar).fullCalendar('renderEvents', temp, true);
      });
  }

  private getData() {
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
