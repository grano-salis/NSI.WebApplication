import {
  ComponentFactoryResolver,
  Injectable,
  Inject,
  ReflectiveInjector,
  ViewContainerRef
} from '@angular/core'

import { DocumentFilterComponent } from '../pages/documents/document-filter-list/document-filter/document-filter.component'

@Injectable()
export class DocumentsFilterService {
  rootViewContainer: ViewContainerRef;

  constructor(private factoryResolver: ComponentFactoryResolver) {
    this.factoryResolver = factoryResolver;
  }

  setRootViewContainerRef(viewContainerRef: ViewContainerRef) {
    this.rootViewContainer = viewContainerRef;
  }

  addDocumentFilterComponent(scopedToCase: boolean) {
    const factory = this.factoryResolver
      .resolveComponentFactory(DocumentFilterComponent);

    const component = factory
      .create(this.rootViewContainer.parentInjector);

    if (scopedToCase) {
      component.instance.scopedToCase = true;
    }
    component.instance._ref = component;

    this.rootViewContainer.insert(component.hostView);

  }
}