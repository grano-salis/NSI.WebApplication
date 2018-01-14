import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentCategoryComponent } from './document-category.component';

describe('DocumentCategoryComponent', () => {
  let component: DocumentCategoryComponent;
  let fixture: ComponentFixture<DocumentCategoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DocumentCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
