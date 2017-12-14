import {Component, OnInit} from '@angular/core';
import {each} from 'lodash';
import * as moment from 'moment';
import {Logger} from '../../../core/services/logger.service';
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

  private color: any = '#223442';

  initCalendar: boolean;
  calendarEvents: any = [];

  constructor(private meetingsService: MeetingsService, private alertService: AlertService,
              private router: Router) {}


  // ****************
  // Public methods
  // ****************
  ngOnInit() {
    this.loadMeetings();
  }

  onEventClicked(event: any) {
    this.router.navigate(['/meetings/edit', event.meetingId]);
  }

  onRangeSelected(range: any) {
    logger.debug("onRangeSelected", range);
    this.router.navigate(['/meetings/new'], {queryParams: {dateFrom: range.start, dateTo: range.end}});
  }

  // ****************
  // Private methods
  // ****************
  private loadMeetings(): any {
    this.meetingsService.getMeetings()
      .subscribe((r: any) => {

        let meetings = r.data;
        each(meetings, (item) => {
          this.calendarEvents.push({
            title: item.title,
            start: item.from,
            end: item.to,
            color: this.color,
            meetingId: item.meetingId
          });
        });

        this.initCalendar = true;
      }, e => {
        this.alertService.showError(e.Message); //TODO: check???
        console.log("error", e);
      });
  }
}
