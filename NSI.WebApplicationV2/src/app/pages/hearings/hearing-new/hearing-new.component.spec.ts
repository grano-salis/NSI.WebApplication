import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HearingNewComponent } from './hearing-new.component';

describe('HearingNewComponent', () => {
  let component: HearingNewComponent;
  let fixture: ComponentFixture<HearingNewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HearingNewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HearingNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
