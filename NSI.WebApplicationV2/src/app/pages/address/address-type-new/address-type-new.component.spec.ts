import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddressTypeNewComponent } from './address-type-new.component';

describe('AddressTypeNewComponent', () => {
  let component: AddressTypeNewComponent;
  let fixture: ComponentFixture<AddressTypeNewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddressTypeNewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddressTypeNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
