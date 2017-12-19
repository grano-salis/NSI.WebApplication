import { 
  Component, 
  Input, 
  Inject,
  OnInit,   
  ViewChild,
  ViewContainerRef 
} from '@angular/core';

import { DocumentsService } from '../../../services/documents.service';
import { DocumentsFilterService } from '../../../services/documents-filter.service';

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

  constructor(private documentsService: DocumentsService, private documentsFilterService: DocumentsFilterService) { }

  ngOnInit() {
      this.documentsFilterService.setRootViewContainerRef(this.viewContainerRef);
      this.documentsFilterService.addDocumentFilterComponent();
      this.documentsService.newFilterEvent.subscribe(() => this.documentsFilterService.addDocumentFilterComponent());
  }
}
