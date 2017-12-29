import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentHistoryModalComponent } from './document-history-modal.component';

describe('DocumentHistoryModalComponent', () => {
  let component: DocumentHistoryModalComponent;
  let fixture: ComponentFixture<DocumentHistoryModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DocumentHistoryModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentHistoryModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
