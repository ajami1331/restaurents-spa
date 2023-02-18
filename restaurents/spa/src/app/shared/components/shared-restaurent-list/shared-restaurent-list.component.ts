import { Component, OnInit } from '@angular/core';
import {RestaurantViewModel} from "../../models/restaurant-view.model";
import {BehaviorSubject, Subscription} from "rxjs";
import {RestaurantsService} from "../../services/restaurants.service";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {mergeMap, tap} from "rxjs/operators";
import {SharedRestaurentCreateComponent} from "../shared-restaurent-create/shared-restaurent-create.component";
import {TokenService} from "../../services/token.service";

@Component({
  selector: 'app-shared-restaurent-list',
  templateUrl: './shared-restaurent-list.component.html',
  styleUrls: ['./shared-restaurent-list.component.scss']
})
export class SharedRestaurentListComponent implements OnInit {
  restaurants: RestaurantViewModel[] = [];
  lowerBound = 0;
  upperBound = 500;
  query = {
    pageSize: 12,
    pageNumber: 0,
    lowerBound: 0,
    upperBound: 5,
  }
  querySubject: BehaviorSubject<any> = new BehaviorSubject(this.query);
  distance: number = 3;
  throttle: number = 400;
  isLoadingResults: boolean = false;
  pageCount = 10;
  private subscriptions: Subscription[] = [];
  private totalCount: number;
  private user: any;
  private userRoles: string[];

  constructor(private restaurantsService: RestaurantsService,
              private dialog: MatDialog,
              private tokenService: TokenService) { }

  ngOnInit(): void {
    this.user = this.tokenService.getUser();
    this.userRoles = this.tokenService.getRoles();
    this.subscriptions.push(
      this.querySubject.pipe(
        tap(() => this.isLoadingResults = true),
        mergeMap(data => this.restaurantsService.getRestaurants(data))
      ).subscribe((res) => {
        this.restaurants.push(...res.data);
        this.pageCount = res.pageCount;
        this.query.pageNumber = res.pageNumber;
        this.query.pageSize = res.pageSize;
        this.totalCount = res.totalCount;
        this.isLoadingResults = false;
      }));
  }
  formatLabel(value: number) {
    return value / 100;
  }
  lowerBoundChanged(value: number) {
    if (this.lowerBound >= this.upperBound) {
      this.upperBound = this.lowerBound;
    }
    this.query.lowerBound = value / 100;
    this.query.pageNumber = 0;
    this.restaurants = []
    this.querySubject.next(this.query);
  }
  upperBoundChanged(value: number) {
    if (this.lowerBound >= this.upperBound) {
      this.lowerBound = this.upperBound;
    }
    this.query.upperBound = value / 100;
    this.query.pageNumber = 0;
    this.restaurants = []
    this.querySubject.next(this.query);
  }

  onScroll() {
    if (this.isLoadingResults) {
      return;
    }
    if (this.query.pageNumber + 1 >= this.pageCount) {
      return;
    }
    this.query.pageNumber++;
    this.querySubject.next(this.query);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  openDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {

    }

    const dialogRef = this.dialog.open(SharedRestaurentCreateComponent, dialogConfig);

    this.subscriptions.push(
      dialogRef.afterClosed().subscribe(
        data => {
          console.log("Dialog output:", data);
          this.restaurants = [];
          this.query.pageNumber = 0;
          this.querySubject.next(this.query);
        }
      ));
  }

  isOwner() {
    return this.userRoles.includes('restaurant_owner');
  }
}
