import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddressNewComponent } from './address-new.component';

describe('AddressNewComponent', () => {
  let component: AddressNewComponent;
  let fixture: ComponentFixture<AddressNewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddressNewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddressNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
