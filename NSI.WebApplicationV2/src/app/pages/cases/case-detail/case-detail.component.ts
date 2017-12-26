import {Component, ElementRef, EventEmitter, Input, Output, ViewChild,OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { CasesService } from '../../../services/cases.service';
import { HearingsService } from '../../../services/hearings.service';
//import { DocumentsService } from '../../../services/documents.service';


@Component({
  //selector: 'app-case-detail',
  templateUrl: './case-detail.component.html',
  //styleUrls: ['./case-detail.component.scss']
})
export class CaseDetailComponent implements OnInit {
  hearing: any;
  document:any;

  pageTitle:string = 'CASE DETAIL';
  @Input() param_case: any;
  id: number;
  model: any;
  param:any;
  case:any;
  hearings : any[];
 
	private sub: any;

  constructor(private _casesService: CasesService,
    private _hearingsService: HearingsService,
    //private _documentsService : DocumentsService,
    private _route:ActivatedRoute,
    private _router:Router) { }

  ngOnInit() {
    /*this.sub = this._route.params.subscribe(params => {
			this.caseId = +params['caseId'];
			this._casesService.getCaseById(this.caseId).subscribe(data => {
				this.model = data;
				console.log(this.model);
			});
    });*/
     this.param=this._route.snapshot.paramMap.get('id');
    if(this.param){
      const id=+this.param;
      this.getCase(id);
      this.getHearing(id);
     // this.getDocument(id);
   
      
    }
  
  }
  getCase(id:number){
    
        this._casesService.getCaseInfoById(id).subscribe(
    
          data => {
            if (data != null) {
              this.case = data;
              console.log('this.case', this.case);
            }
          });
        
      }
     

      getHearing(id:number){
        this._hearingsService.getHearingsByCase(id).subscribe(
          
                data=> {
                  if (data != null) {
                   //this.hearing = data;
                  
               
                   // console.log('this.hearing', this.hearing);
                    this.hearings = data.data;
                    this.hearings=Array.of(this.hearings);
                    console.log('All: ' + JSON.stringify(this.hearings));
                  }
                });
           
            
          } 

         /* getDocument(id:number){
            console.log('nbilo sta');
            this._documentsService.getDocumentsByCase(id).subscribe(
               
                    data=> {
                      console.log(data);
                      if (data != null) {
                        this.document = data;
                        
                       // console.log('this.hearing', this.hearing);
                        console.log('All doc: ' +  this.document);
                      }
                    });
               
                
              } */

  onBack() : void {
    this._router.navigate(['cases/all']);

  }
}
