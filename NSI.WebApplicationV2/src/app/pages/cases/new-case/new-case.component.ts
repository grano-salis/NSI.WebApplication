import { Component, OnInit } from '@angular/core';
import {Case} from './case';
import {CasesService} from '../../../services/cases.service';

@Component({
  selector: 'app-new-case',
  templateUrl: './new-case.component.html',
  styleUrls: ['./new-case.component.scss']
})
export class NewCaseComponent implements OnInit {

  model: Case;

  constructor(private casesService: CasesService) { }

  ngOnInit() {
    this.model = new Case();
  }

  onSubmit() {
    console.log('this.model.judge',this.model.judge)
    this.model.dateCreated = new Date().toLocaleDateString();
    this.model.dateModified = null;
    this.model.caseCategory = 1;
    this.model.customerId = 1;
    this.model.clientId = 1;
    this.model.createdByUserId = 1;
    console.log('this.model',this.model)
    this.casesService.postCase(this.model).subscribe(data =>{
      console.log('data',data);
    });
  }
}
