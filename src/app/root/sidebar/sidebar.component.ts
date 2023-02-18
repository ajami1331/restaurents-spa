import {Component, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {MatSidenav} from "@angular/material/sidenav";
import {BreakpointObserver} from "@angular/cdk/layout";
import {SideNavService} from "../../shared/services/side-nav.service";
import {Subscription} from "rxjs";
import {TokenService} from "../../shared/services/token.service";
import {NavItem} from "../../shared/models/nav-items.model";
import { menu } from "./menu";
import {NavService} from "../../shared/services/nav.service";

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit, OnDestroy {
  @ViewChild(MatSidenav) sidenav: MatSidenav;
  user: any = null;
  private subscriptions: Subscription[] = [];
  menuItems: NavItem[] = menu;
  private currentUrl: string = '';
  constructor(private observer: BreakpointObserver, private sideNavService: SideNavService, private tokenService: TokenService, private navService: NavService) {}

  ngAfterViewInit() {
    this.subscriptions.push(
      this.sideNavService.sideNavToggleSubject.subscribe((isOpen)=> {
        if (isOpen) {
          this.sidenav.mode = 'side';
          this.sidenav.open();
        } else {
          this.sidenav.mode = 'over';
          this.sidenav.close();
        }
    }));
    this.subscriptions.push(this.observer.observe(['(max-width: 800px)']).subscribe((res) => {
      if (res.matches) {
        this.sideNavService.close();
      } else {
        this.sideNavService.open();
      }
    }));
    this.user = this.tokenService.getUser();
    console.log(this.user);
  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.navService.currentUrl.subscribe((currentUrl)=> {
        this.currentUrl = currentUrl;
        console.log(currentUrl);
        console.log(this.menuItems);
    }));
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }


  isSelected(menuItem: NavItem) {
    return this.currentUrl.startsWith(menuItem.route) ? 'primary' : 'accent';
  }

  getDesignation() {
    if (this.user.role.includes('restaurant_owner')) {
      return 'Restaurant Owner';
    }
    if (this.user.role.includes('user')) {
      return 'User';
    }
    if (this.user.role.includes('admin')) {
      return 'Admin';
    }
  }

  canAccess(menuItem: NavItem) {
    if (!this.user) {
      return false;
    }
    for (let role of menuItem.roles) {
      if (this.user.role.includes(role)) {
        return true;
      }
    }
    return false;
  }
}
