import { Component, OnInit, NgModule } from '@angular/core';
import {CasesService} from '../../../services/cases.service';
import {Router} from "@angular/router";
// import{MeetingsModule} from "../../meetings/meetings.module";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})


export class DashboardComponent implements OnInit {

  latestCasesList: any;


  constructor(private casesService: CasesService,
              private router: Router) { }

  ngOnInit() {
    this.loadLatestCases();
  }

  loadLatestCases() {
    this.casesService.getLatestCases().subscribe(data => {
      if (data != null) {
        this.latestCasesList = data;
        console.log('this.casesList', this.latestCasesList);
      }
    });
  }


}
