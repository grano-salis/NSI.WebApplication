import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingNewComponent } from './meeting-new.component';

describe('MeetingNewComponent', () => {
  let component: MeetingNewComponent;
  let fixture: ComponentFixture<MeetingNewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MeetingNewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MeetingNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
