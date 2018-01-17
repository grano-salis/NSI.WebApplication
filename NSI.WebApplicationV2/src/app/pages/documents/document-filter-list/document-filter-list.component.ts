import { 
  Component, 
  Input, 
  Inject,
  OnInit,   
  ViewChild,
  ViewChildren,
  ViewContainerRef,
  QueryList,
  ContentChild,
  ContentChildren,
  EmbeddedViewRef
} from '@angular/core';

import { DocumentFilter } from '../models/documentFilter.model'
import { DocumentsService } from '../../../services/documents.service';
import { DocumentsFilterService } from '../../../services/documents-filter.service';
import { DocumentFilterComponent } from './document-filter/document-filter.component';

@Component({
  selector: 'app-document-filter-list',
  templateUrl: './document-filter-list.component.html',
  styleUrls: ['./document-filter-list.component.css']
})
export class DocumentFilterListComponent implements OnInit {
  @Input() scopedToCase: boolean;

  @ViewChild('filterComponentList', { 
    read: ViewContainerRef 
  }) viewContainerRef: ViewContainerRef;

  @ViewChildren('filterComponentList') filters: QueryList<DocumentFilterComponent>;

  constructor(private documentsService: DocumentsService, private documentsFilterService: DocumentsFilterService) { }

  ngOnInit() {
      this.documentsFilterService.setRootViewContainerRef(this.viewContainerRef);
      this.documentsFilterService.addDocumentFilterComponent(this.scopedToCase);
      
      this.documentsService.newFilterEvent
        .subscribe(() => this.documentsFilterService.addDocumentFilterComponent(this.scopedToCase));
  }

  onFilterSearch() {
      this.documentsService.submitFiltering.next();
  }
}
