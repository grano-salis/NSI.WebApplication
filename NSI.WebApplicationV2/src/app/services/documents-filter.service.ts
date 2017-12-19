import {
  ComponentFactoryResolver,
  Injectable,
  Inject,
  ReflectiveInjector,
  ViewContainerRef
} from '@angular/core'

import { DocumentFilterComponent } from '../pages/documents/document-filter/document-filter.component'

@Injectable()
export class DocumentsFilterService {
  rootViewContainer: ViewContainerRef;

  constructor(private factoryResolver: ComponentFactoryResolver) {
    this.factoryResolver = factoryResolver;
  }

  setRootViewContainerRef(viewContainerRef: ViewContainerRef) {
    this.rootViewContainer = viewContainerRef;
  }

  addDocumentFilterComponent() {
    const factory = this.factoryResolver
      .resolveComponentFactory(DocumentFilterComponent);

    const component = factory
      .create(this.rootViewContainer.parentInjector);
    component.instance._ref = component;

    this.rootViewContainer.insert(component.hostView);

  }
}