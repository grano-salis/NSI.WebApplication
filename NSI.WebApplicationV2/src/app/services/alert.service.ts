import { Injectable } from '@angular/core';
import {ToastrService} from "ngx-toastr";

@Injectable()
export class AlertService {

  constructor(private toastr: ToastrService) { }

  showError(title: string, body?: string) {
    this.toastr.error(title, body);
  }
}
