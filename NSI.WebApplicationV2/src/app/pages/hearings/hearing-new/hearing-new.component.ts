import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Hearing } from './hearing';
import { HearingsService } from '../../../services/hearings.service';
import { UsersService } from '../../../services/users.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from "../../../services/alert.service";
import { DatePipe } from '@angular/common';


declare var $: any;

@Component({
  selector: 'app-hearing-new',
  templateUrl: './hearing-new.component.html',
  styleUrls: ['./hearing-new.component.scss'],
  providers: [DatePipe]
})
export class HearingNewComponent implements OnInit, AfterViewInit {

  ngAfterViewInit(): void {
    let self = this;
    $('#hearingDate').datetimepicker({ useCurrent: false, format: "MM/DD/YYYY, HH:mm:ss" });
    $("#hearingDate").on("dp.change", function (e: any) {
      self.model.hearingDate = $("#hearingDate").val();
    });
  }

  query: string;
  filteredList: string[];
  notes: { text: string, createdByUserId: number, hearingId: number }[];
  model: Hearing;
  date: string;
  noteText: string;
  id: number;
  noteIndex: number;
  public edit: boolean = false;

  constructor(private hearingsService: HearingsService, private usersService: UsersService, private route: ActivatedRoute,
    private router: Router, private alertService: AlertService, private datePipe: DatePipe) {
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
  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id');
    if (this.id != 0) {
      this.hearingsService.getHearingById(this.id).subscribe(response => {
        let data = response.data;
        if (data != null) {
          console.log(data);
          this.edit = true;
          console.log(data.hearingDate);
          console.log(new Date(data.hearingDate));
          this.model.hearingDate = this.datePipe.transform(new Date(data.hearingDate), 'MM/dd/yyyy, HH:mm:ss');
          this.model.userHearing = data.userHearing;
          this.model.note = data.note;
          this.noteText = data.note[0].text;
          $("#editor-one").html(this.noteText);
        }
        console.log(this.edit);
      },
        (error: any) => {
          this.alertService.showError(error.error.message)
        }
      );
    }
  }

  updateHearing() {
    this.model.hearingDate = $('#hearingDate').val();
    this.noteIndex = this.model.note.findIndex(x => x.createdByUserId == 1);
    if(this.noteIndex != null)
    {
      this.model.note.splice(this.noteIndex, 1,{ text: this.noteText, createdByUserId: 1, hearingId: 5 });
    }
    else
    {
      this.model.note.push({ text: this.noteText, createdByUserId: 2, hearingId: 5 });
    }
    this.hearingsService.putHearing(this.id, this.model).subscribe((r: any) => this.alertService.showSuccess("Success", "Hearing updated"),
      (error: any) => this.alertService.showError(error.error.message));
  }

  onSubmit() {
    console.log("Form submitted");
    this.noteText = $.trim($("#editor-one").html())
    console.log(this.noteText)
    this.model.hearingDate = $('#hearingDate').val()
    this.notes.push({ text: this.noteText, createdByUserId: 1, hearingId: 5 })
    this.model.note = this.notes
    this.model.createdByUserId = 1
    this.model.caseId = 3
    console.log(this.model);
    this.hearingsService.postHearing(this.model).subscribe((r: any) => {
      this.alertService.showSuccess("Success", "Hearing created")
      // this.model = new Hearing();
    },
      (error: any) => this.alertService.showError(error.error.message));
  }

  newHearing() {
    this.model = new Hearing();
    this.router.navigate(['/hearings/new']);
  }

  deleteHearing() {
    this.hearingsService.deleteHearingById(this.id).subscribe((r: any) => {
      this.model = new Hearing();
      this.alertService.showSuccess("Success", "Hearing deleted");
    }, (error: any) => this.alertService.showError(error.error.message));
  }

}
