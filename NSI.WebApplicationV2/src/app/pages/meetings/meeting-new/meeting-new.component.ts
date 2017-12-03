import { Component } from '@angular/core';
import { Meeting } from './meeting';
import { MeetingsService } from '../../../services/meetings.service';
import { UsersService } from '../../../services/users.service';

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

  constructor(private meetingsService: MeetingsService, private usersService: UsersService) {
    this.query = '';
    this.filteredList = [];
    this.model = new Meeting();
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
    console.log("Form submitted");
    this.model.from = '01/01/2001';
    this.model.to = '01/01/2002';
    this.meetingsService.postMeeting(this.model).subscribe((r: any) => console.log('Hazime imamo bingo: ' + r),
      (error: any) => console.log("Error: " + error.message));
  }

  newMeeting() {
    this.model = new Meeting();
  }

}
