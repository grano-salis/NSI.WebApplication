import {Component, ElementRef, EventEmitter, Input, Output, ViewChild,OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { CasesService } from '../../../services/cases.service';
import { HearingsService } from '../../../services/hearings.service';
import { DocumentsService } from '../../../services/documents.service';
import { ContactsService } from '../../../services/contacts.service';


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
  docs:any[];
  contacts : any[];
  taskTabOpenName : any;
  noteText:string;
 
	private sub: any;

  constructor(private _casesService: CasesService,
    private _hearingsService: HearingsService,
    private _documentsService : DocumentsService,
    private _contactsService : ContactsService,
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
      this.getDocument(id);
      this.getDocuments(id);
      this.getContacts(id);
   
      
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
                  
                    this.model.hearingDate = data.hearingDate;
                    this.model.userHearing = data.userHearing;
                    this.model.note = data.note;
                    this.noteText = data.note[0].text;
                    this.hearings=Array.of(this.hearings);

                    this.hearings=Array.of(this.hearings);
                    console.log('All: ' + JSON.stringify(this.hearings));
                  }
                });
           
            
          } 
          getDocuments(id:number){
            console.log('mymy sta');
            this._documentsService.getDocumentsByCase(id).subscribe(
              
                    data=> {
                      if (data != null) {
                       //this.hearing = data;
                      
                   
                       // console.log('this.hearing', this.hearing);
                        this.docs = data;
                       // this.docs=Array.of(this.docs);
                        console.log('All documents for case : ',this.docs);
                      }
                    });
               
                
              } 

          getDocument(id:number){
            console.log('nbilo sta');
            this._documentsService.getNumberOfDocumentsByCase(id).subscribe(
               
                    data=> {
                      console.log(data);
                      if (data != null) {
                        this.document = data;
                        
                       // console.log('this.hearing', this.hearing);
                        console.log('All doc: ' +  this.document);
                      }
                    });
               
                
              } 
              getContacts(id:number){
                console.log('kontakti');
                this._contactsService.getContactsByCase(id).subscribe(
                  
                        data=> {
                          if (data != null) {
                           //this.hearing = data;
                          
                       
                           // console.log('this.hearing', this.hearing);
                            this.contacts = data;
                           // this.docs=Array.of(this.docs);
                            console.log('All documents for case : ',this.contacts);
                          }
                        });
                   
                    
                  } 
              getTab(tabName : string) {
                if (tabName === 'Hearings') {
                  this.taskTabOpenName = 'Hearings';
                  document.getElementById('sectionTabOpen').className = 'col-lg-3 col-md-3 col-xs-3 primaryTabStyle';
                  document.getElementById('sectionGroupsTabOpen').className = 'col-lg-3 col-md-3 col-xs-3 secondaryTabStyle';
                  document.getElementById('DocumentsTabOpen').className = 'col-lg-3 col-md-3 col-xs-3 secondaryTabStyle';
                }
                else if (tabName === 'Contacts'){
                  this.taskTabOpenName = 'Contacts';
                  document.getElementById('sectionTabOpen').className = 'col-lg-3 col-md-3 col-xs-3 secondaryTabStyle';
                  document.getElementById('DocumentsTabOpen').className = 'col-lg-3 col-md-3 col-xs-3 secondaryTabStyle';
                  document.getElementById('sectionGroupsTabOpen').className = 'col-lg-3 col-md-3 col-xs-3 primaryTabStyle';
                } else if (tabName === 'Documents'){
                  this.taskTabOpenName = 'Documents';
                  document.getElementById('sectionTabOpen').className = 'col-lg-3 col-md-3 col-xs-3 secondaryTabStyle';
                  document.getElementById('sectionGroupsTabOpen').className = 'col-lg-3 col-md-3 col-xs-3 secondaryTabStyle';
                  document.getElementById('DocumentsTabOpen').className = 'col-lg-3 col-md-3 col-xs-3 primaryTabStyle';
                }
                else if (tabName === 'Notes'){
                  this.taskTabOpenName = 'Notes';
                  document.getElementById('sectionTabOpen').className = 'col-lg-3 col-md-3 col-xs-3 secondaryTabStyle';
                  document.getElementById('sectionGroupsTabOpen').className = 'col-lg-3 col-md-3 col-xs-3 secondaryTabStyle';
                  document.getElementById('NotesTabOpen').className = 'col-lg-3 col-md-3 col-xs-3 primaryTabStyle';
                }
                
              }          

  onBack() : void {
    this._router.navigate(['cases/all']);

  }
}
