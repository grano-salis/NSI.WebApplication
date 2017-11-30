import { Component } from '@angular/core';
import { Meeting } from './meeting';

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

  constructor() {
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

  add(item:string) {
    this.model.users.push(item);
    this.query = '';
    this.filteredList = [];
  }

  remove(item:string){
    this.model.users.splice(this.model.users.indexOf(item),1);
}

  onSubmit() {
    console.log("Form submitted");
    console.log(this.model);
  }

  newMeeting() {
    this.model = new Meeting();
  }

}
