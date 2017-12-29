import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NsiCalendarComponent } from './nsi-calendar.component';

describe('NsiCalendarComponent', () => {
  let component: NsiCalendarComponent;
  let fixture: ComponentFixture<NsiCalendarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NsiCalendarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NsiCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
