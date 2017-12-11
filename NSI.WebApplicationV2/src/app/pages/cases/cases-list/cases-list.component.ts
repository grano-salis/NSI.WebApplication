import { Component, OnInit } from '@angular/core';
import {CasesService} from '../../../services/cases.service';
import {forEach} from '@angular/router/src/utils/collection';
import {element} from 'protractor';

@Component({
  selector: 'app-cases-list',
  templateUrl: './cases-list.component.html',
  styleUrls: ['./cases-list.component.scss']
})
export class CasesListComponent implements OnInit {

  casesList: any;

  constructor(private casesService: CasesService) { }

  ngOnInit() {
    this.loadAllCases();
  }


  loadAllCases(){
    this.casesService.getCases().subscribe(data =>{
      if(data!=null){
        this.casesList = data;
        console.log('this.casesList',this.casesList);
      }
    });
  }

}
