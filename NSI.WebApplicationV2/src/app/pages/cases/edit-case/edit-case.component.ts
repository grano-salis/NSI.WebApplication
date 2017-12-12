import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CasesService} from '../../../services/cases.service';
import {Case} from "../new-case/case";

@Component({
  selector: 'app-edit-case',
  templateUrl: './edit-case.component.html',
  styleUrls: ['./edit-case.component.scss']
})
export class EditCaseComponent implements OnInit {

  caseToEditId: any;
  model: any;
  constructor(private router: Router,
              private route: ActivatedRoute,
              private caseService: CasesService) { }

  ngOnInit() {
    this.model = new Case();
    this.caseToEditId = +this.route.snapshot.paramMap.get('caseId');
    console.log('this.caseToEditId', this.caseToEditId);
    this.loadCase();
  }

  loadCase(): any {
    this.caseService.getCaseById(this.caseToEditId).subscribe( data => {
      console.log('dataedit', data)
      this.model = data;
    });
  }

  onSubmit() {
    console.log('this.model', this.model);
    // this.model.dateCreated = new Date().toLocaleDateString();
    // this.model.dateModified = null;
    // this.model.caseCategory = 1;
    // this.model.customerId = 1;
    // this.model.clientId = 1;
    // this.model.createdByUserId = 1;
    // console.log('this.model',this.model)
    // this.casesService.postCase(this.model).subscribe(data =>{
    //   console.log('data',data);
    //   this.router.navigate(['cases/all']);
    // });
  }
}
