import { Component } from '@angular/core';
import { Meeting } from './meeting';
import { MeetingsService } from '../../../services/meetings.service';

@Component({
  selector: 'app-meeting-new',
  templateUrl: './meeting-new.component.html',
  styleUrls: ['./meeting-new.component.scss']
})
export class MeetingNewComponent {

  users = ["user 1", "user 2", "user 3", "user 5", "user 4"]

  query: string;
  filteredList: string[];
  model: Meeting;

  constructor(private meetingsService: MeetingsService) {
    this.query = '';
    this.filteredList = [];
    this.model = new Meeting();
  }

  filter() {
    if (this.query.length > 2) {
      this.filteredList = this.users.filter(function (el: string) {
        return el.toLowerCase().indexOf(this.query.toLowerCase()) > -1;
      }.bind(this));
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
    console.log("Form submitted");
    console.log(this.model);
    // Submit fake data
    this.model.from = '01/01/2001';
    this.model.to = '01/01/2002'
    this.model.userMeeting = [
      {
        'userId': 1,
        'userName': 'admin'
      }
    ];
    this.meetingsService.postMeeting(this.model).subscribe((r: any) => console.log('Hazime imamo bingo: ' + r),
      (error: any) => console.log("Error: " + error.message));
  }

  newMeeting() {
    this.model = new Meeting();
  }

}
