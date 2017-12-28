import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Meeting } from './meeting';
import { MeetingsService } from '../../../services/meetings.service';
import { UsersService } from '../../../services/users.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from "../../../services/alert.service";
import { DatePipe } from '@angular/common';

declare var $: any;

@Component({
  selector: 'app-meeting-new',
  templateUrl: './meeting-new.component.html',
  styleUrls: ['./meeting-new.component.scss'],
  providers: [DatePipe]
})
export class MeetingNewComponent implements OnInit, AfterViewInit {

  ngAfterViewInit(): void {
    let self = this;
    $('#from').datetimepicker({
      format: "MM/DD/YYYY, HH:mm:ss"
    });
    $('#to').datetimepicker({
      useCurrent: false, //Important! See issue #1075
      format: "MM/DD/YYYY, HH:mm:ss"
    });
    $("#from").on("dp.change", function (e: any) {
      self.model.from = $("#from").val();
      $('#to').data("DateTimePicker").minDate(e.date);
    });
    $("#to").on("dp.change", function (e: any) {
      self.model.to = $("#to").val();
      $('#from').data("DateTimePicker").maxDate(e.date);
    });
  }

  query: string;
  filteredList: string[];
  model: Meeting;
  id: number;
  public edit: boolean = false;
  usersAvailableForMeeting : any[];
  availableMeetings : any[];

  constructor(private meetingsService: MeetingsService, private usersService: UsersService, private route: ActivatedRoute,
    private router: Router, private alertService: AlertService, private datePipe: DatePipe) {
    this.query = '';
    this.filteredList = [];
    this.model = new Meeting();
    this.usersAvailableForMeeting = [];
    this.availableMeetings = [];
  }


  filter() {
    if (this.query.length > 2) {
      this.usersService.getForMeetings(this.query).subscribe(data => {
        console.log(data);
        this.filteredList = data;
      })
    } else {
      this.filteredList = [];
    }
  }

  add(item: string) {
    this.model.userMeeting.push(item);
    this.query = '';
    this.filteredList = [];
  }

  remove(item: string) {
    this.model.userMeeting.splice(this.model.userMeeting.indexOf(item), 1);
  }

  onSubmit() {
    console.log(this.model);
    this.model.from = $('#from').val();
    this.model.to = $('#to').val();
    console.log(this.model);
    this.meetingsService.postMeeting(this.model).subscribe((r: any) => this.router.navigate(['/meetings'], { queryParams: { frommeeting: "created" } }),
      (error: any) => this.alertService.showError(error.error.message));

  }

  newMeeting() {
    this.model = new Meeting();
    this.router.navigate(['/meetings/new']);
  }

  //edit-update
  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id');
    console.log(this.id);
    if (this.id != 0) {
      this.meetingsService.getMeetingById(this.id).subscribe(response => {
        let data = response.data;
        if (data != null) {
          this.edit = true;
          console.log(data);
          this.model.title = data.title;
          this.model.meetingPlace = data.meetingPlace;
          this.model.from = this.datePipe.transform(new Date(data.from), 'MM/dd/yyyy, HH:mm:ss');
          this.model.to = this.datePipe.transform(new Date(data.to), 'MM/dd/yyyy, HH:mm:ss');
          this.model.userMeeting = data.userMeeting;
        }
        console.log(this.edit);
      });
    }
    /*let dateFrom = this.route.snapshot.queryParamMap.get("dateFrom");
    let dateTo = this.route.snapshot.queryParamMap.get("dateTo");

    if (dateFrom && dateTo) {
      this.model.from = this.datePipe.transform(new Date(dateFrom), 'MM/DD/YYYY, HH:mm:ss');
      this.model.to = this.datePipe.transform(new Date(dateTo), 'MM/DD/YYYY, HH:mm:ss');
    }*/
  }

  updateMeeting() {
    this.model.from = $('#from').val();
    this.model.to = $('#to').val();
    console.log(this.model);

    this.meetingsService.putMeeting(this.id, this.model).subscribe((r: any) => this.router.navigate(['/meetings'], { queryParams: { frommeeting: "update" } }),
      (error: any) => this.alertService.showError(error.error.message));

  }

  deleteMeeting() {
    console.log(this.id);
    this.meetingsService.deleteMeetingById(this.id).subscribe((r: any) => this.router.navigate(['/meetings'], { queryParams: { frommeeting: "delete" } }),
      (error: any) => this.alertService.showError(error.error.message));
  }

  checkUsersAvailability() {
    this.meetingsService.checkUsersAvailability(this.model.userMeeting,this.model.from,this.model.to)
        .subscribe(
          (r: any) => {
            this.usersAvailableForMeeting = r.data; 
          }
        );
    this.getMeetingTimes();        
  }

  getMeetingTimes() {
    this.meetingsService.getMeetingTimes(this.model.userMeeting, this.model.from, this.model.to, 3)
        .subscribe(
          (r: any) => {
            this.availableMeetings = r.data;
          }
        )
  }
}
