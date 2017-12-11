import {Component, OnInit} from '@angular/core';
import {TasksService} from '../../../services/tasks.service';
import {each} from 'lodash';
import * as moment from 'moment';
import {Logger} from '../../../core/services/logger.service';
import {HelperService} from "../../../services/helper.service";
import {MeetingsService} from "../../../services/meetings.service";
import {AlertService} from "../../../services/alert.service";
import {Router} from "@angular/router";

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


  constructor(private meetingsService: MeetingsService, private alertService: AlertService,
              private router: Router) {

    this.calendarConfiguration.select = (start: any, end: any) => this._onSelect(start, end);
  }


  // ****************
  // Public methods
  // ****************
  ngOnInit() {
    this.loadMeetings();
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
      this.router.navigate(['/meetings/new'], {queryParams: {dateFrom: start, dateTo: end}});
    }
  }

  private loadMeetings(): any {
    this.meetingsService.getMeetings()
      .subscribe((r: any) => {
        const temp: any[] = [];
        let meetings = r.data;
        each(meetings, (item) => {
          temp.push({
            title: 'Meeting ID: ' + item.meetingId,
            start: item.from,
            end: item.to,
            color: this.color
          });
        });

        $(this._calendar).fullCalendar('renderEvents', temp, true);
      }, e => {
        this.alertService.showError(e.Message); //TODO: check???
        console.log("error", e);
      });
  }
}
