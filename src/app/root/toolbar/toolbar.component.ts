import {Component, ElementRef, OnDestroy, OnInit, ViewChild} from '@angular/core';
import { environment } from "../../../environments/environment";
import {AuthService} from "../../shared/services/auth.service";
import {Router} from "@angular/router";
import {MatSidenav} from "@angular/material/sidenav";
import {SideNavService} from "../../shared/services/side-nav.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit, OnDestroy {
  appName: string = environment.appName;
  isSidenavOpen: boolean;
  private subscriptions: Subscription[] = [];

  constructor(private authService: AuthService, private router: Router, private sideNavService: SideNavService) { }

  ngOnInit(): void {

  }

  ngAfterViewInit() {
    this.subscriptions.push(
      this.sideNavService.sideNavToggleSubject.subscribe((isOpen)=> {
        this.isSidenavOpen = isOpen;
      }));
  }


  ngOnDestroy(): void {
  }

  public sidebarToggle() {
    if (this.isSidenavOpen) {
      this.sideNavService.close();
    } else {
      this.sideNavService.open();
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']).then(_ => console.log('Logout'));
  }
}
