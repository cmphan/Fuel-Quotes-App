import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';
@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

constructor() { }

confirm(message: string, okCallBack: () => any) {
  alertify.confirm(message, (e: any) => {
    if (e) {
      okCallBack();
    } else {}
  });
}
success(message: string) {
  alertify.success(message);
}
}
