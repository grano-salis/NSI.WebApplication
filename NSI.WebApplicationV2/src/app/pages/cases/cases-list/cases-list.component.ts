import { Component, OnInit, Input } from '@angular/core';
import {CasesService} from '../../../services/cases.service';
import {Router} from "@angular/router";
import { CaseDetail } from '../case-detail/caseDetail.model';

declare let $: any;

@Component({
  selector: 'app-cases-list',
  templateUrl: './cases-list.component.html',
  styleUrls: ['./cases-list.component.scss']
})
export class CasesListComponent implements OnInit {

  casesList: any;
  param_case : any;
  case:CaseDetail[];


  constructor(private casesService: CasesService,
              private router: Router) { }

  ngOnInit() {
    this.loadAllCases();
  }


  editCase(caseId: any) {
    console.log('caseId', caseId);
    this.router.navigate(['cases/edit', caseId]);
    // this.casesService.putCase(caseId).subscribe(data => {
    //   console.log('delete', data);
    //   this.loadAllCases();
    // });
  }

  deleteCase(caseId: any) {
    console.log('caseId', caseId);
    this.casesService.deleteCase(caseId).subscribe(data => {
      console.log('delete', data);
      this.loadAllCases();
    });
  }
 /*deleteCase(caseId : any) {  
    this.casesService.deleteCase(caseId).subscribe(p=> {
        console.log(p);
        
        this.casesList.filter( case => case.caseId !== caseId)
        
       // this.loadAllCases();
        // or you can use splice by using the index
    });
}*/

  loadAllCases() {
    this.casesService.getCases().subscribe(data => {
      if (data != null) {
        this.casesList = data;
        console.log('this.casesList', this.casesList);
      }
    });
  }

  onCaseClick(id: number) {
    
		this.router.navigate([`/cases/${id}`]);
  }
  


}





