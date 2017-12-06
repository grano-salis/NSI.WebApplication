import { Component, OnInit } from '@angular/core';
import { Hearing } from './hearing';
import { HearingsService } from '../../../services/hearings.service';
import { UsersService } from '../../../services/users.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-hearing-new',
  templateUrl: './hearing-new.component.html',
  styleUrls: ['./hearing-new.component.scss']
})
export class HearingNewComponent {

    users = ["user 1", "user 2", "user 3", "user 5", "user 4"]
  
    query: string;
    filteredList: string[];
    notes: {text:string, createdByUserId:number, hearingId:number}[];
    model: Hearing;
    date: string;
    noteText: string;
    id: number;
    public edit: boolean = false;
  
    constructor(private hearingsService: HearingsService, private usersService: UsersService, private route: ActivatedRoute) {
      this.query = '';
      this.filteredList = [];
      this.notes = [];
      this.model = new Hearing();
    }
  
    filter() {
      if (this.query.length > 2) {
        this.usersService.getForHearings(this.query).subscribe(data => {
          console.log(data);
          this.filteredList = data;
        })
      } else {
        this.filteredList = [];
      }
    }
  
    add(item: string) {
      this.model.userHearing.push(item);
      this.query = '';
      this.filteredList = [];
    }
  
    remove(item: string) {
      this.model.userHearing.splice(this.model.userHearing.indexOf(item), 1);
    }

    //edit-update
    ngOnInit(){
      this.id = +this.route.snapshot.paramMap.get('id');
      this.hearingsService.getHearingById(this.id).subscribe(data => {
        if (data != null)
        {
          console.log(data);
          this.edit = true;
          this.model.hearingDate = data.hearingDate;
          this.model.userHearing = data.userHearing;
          this.model.note = data.note;
        }
        console.log(this.edit);
      });
    }

    updateHearing(){
      this.hearingsService.putHearing(this.id, this.model).subscribe((r: any) => console.log('Saljemo update: ' + r),
      (error: any) => console.log("Error: " + error.message));
    }
  
    onSubmit() {
      console.log("Form submitted");
      this.notes.push({ text: this.noteText, createdByUserId: 1, hearingId: 5});
      this.model.note = this.notes;
      this.model.hearingDate = this.date;
      this.model.createdByUserId = 1;
      this.model.caseId = 3;
      this.hearingsService.postHearing(this.model).subscribe((r: any) => console.log('Hazime imamo bingo: ' + r),
        (error: any) => console.log("Error: " + error.message));
    }
  
    newHearing() {
      this.model = new Hearing();
    }

    deleteHearing() {
      this.hearingsService.deleteHearingById(this.id).subscribe((r: any) => console.log('Brisemo hearing:' + r),
        (error: any) => console.log("Error: " + error.message));
    }

}
