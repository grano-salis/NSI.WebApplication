import { Injectable } from '@angular/core';

@Injectable()
export class HelperService {

  constructor() { }

  static scrollToTop() {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
  }

}
