import { Injectable } from '@angular/core';
import { Event, NavigationEnd, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import {Location} from "@angular/common";

@Injectable({ providedIn: 'root' })
export class NavService {
  public currentUrl = new BehaviorSubject<string>(undefined);
  private history: string[] = [];
  constructor(private router: Router, private location: Location) {
    this.router.events.subscribe((event: Event) => {
      if (event instanceof NavigationEnd) {
        this.currentUrl.next(event.urlAfterRedirects);
        this.history.push(event.urlAfterRedirects)
      }
    });
  }
  back(): void {
    this.history.pop()
    if (this.history.length > 0) {
      this.location.back();
    } else {
      this.router.navigateByUrl('/')
    }
  }
}
