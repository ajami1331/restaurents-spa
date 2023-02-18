import { Injectable } from '@angular/core';
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SideNavService {

  public sideNavToggleSubject: BehaviorSubject<any> = new BehaviorSubject(true);
  constructor() {}

  public close() {
    return this.sideNavToggleSubject.next(false);
  }

  public open() {
    return this.sideNavToggleSubject.next(true);
  }
}
