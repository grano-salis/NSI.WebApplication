import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FileTypeComponent } from './file-type.component';

describe('FileTypeComponent', () => {
  let component: FileTypeComponent;
  let fixture: ComponentFixture<FileTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FileTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FileTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
