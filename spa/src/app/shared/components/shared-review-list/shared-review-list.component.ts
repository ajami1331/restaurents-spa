import { Component, OnInit } from '@angular/core';
import {ReviewViewModel} from "../../models/review-view.model";
import {BehaviorSubject, forkJoin, of, Subscription} from "rxjs";
import {ReviewService} from "../../services/review.service";
import {RestaurantsService} from "../../services/restaurants.service";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {filter, mergeMap, tap} from "rxjs/operators";
import {SharedReviewReplyComponent} from "../shared-review-reply/shared-review-reply.component";
import {TokenService} from "../../services/token.service";
import {SharedModalYesNoComponent} from "../shared-modal-yes-no/shared-modal-yes-no.component";
import {SharedEditReviewComponent} from "../shared-edit-review/shared-edit-review.component";

@Component({
  selector: 'app-shared-review-list',
  templateUrl: './shared-review-list.component.html',
  styleUrls: ['./shared-review-list.component.scss']
})
export class SharedReviewListComponent implements OnInit {
  reviews: ReviewViewModel[] = [];
  query = {
    pageSize: 12,
    pageNumber: 0,
  }
  querySubject: BehaviorSubject<any> = new BehaviorSubject(this.query);
  restaurantNameSubject: BehaviorSubject<string> = new BehaviorSubject(null);
  distance: number = 3;
  throttle: number = 400;
  isLoadingResults: boolean = false;
  pageCount = 10;
  private subscriptions: Subscription[] = [];
  private totalCount: any;
  restaurantNames: [] = [];
  requestedRestaurantNames: [] = [];
  private user: any;
  private userRoles: string[];

  constructor(private reviewService: ReviewService,
              private restaurantsService: RestaurantsService,
              private dialog: MatDialog,
              private tokenService: TokenService) { }

  ngOnInit(): void {
    this.user = this.tokenService.getUser();
    this.userRoles = this.tokenService.getRoles();
    this.subscriptions.push(
      this.querySubject.pipe(
        tap(() => this.isLoadingResults = true),
        mergeMap(data => this.reviewService.getReviews(data))
      ).subscribe((res) => {
        this.reviews.push(...res.data);
        this.pageCount = res.pageCount;
        this.query.pageNumber = res.pageNumber;
        this.query.pageSize = res.pageSize;
        this.totalCount = res.totalCount;
        this.isLoadingResults = false;
      }));
    this.subscriptions.push(
      this.restaurantNameSubject.pipe(
        filter(data => data !== null),
        mergeMap((data) => forkJoin([of(data),this.restaurantsService.getRestaurantName(data)])),
      ).subscribe(([name, res]) => this.restaurantNames[name] = res));
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

  getRestaurantName(restaurantId: string) {
    if (this.restaurantNames[restaurantId]) {
      return this.restaurantNames[restaurantId];
    }
    if (!this.requestedRestaurantNames[restaurantId]) {
      this.restaurantNameSubject.next(restaurantId)
      this.requestedRestaurantNames[restaurantId] = true;
    }
    return '';
  }

  openDialog(review: ReviewViewModel) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      review: {
        ...review
      }
    }

    const dialogRef = this.dialog.open(SharedReviewReplyComponent, dialogConfig);

    this.subscriptions.push(
      dialogRef.afterClosed().subscribe(
        data => {
          console.log("Dialog output:", data);
          this.reviews = [];
          this.query.pageNumber = 0;
          this.querySubject.next(this.query);
        }
      ));
  }



  delete(review: ReviewViewModel) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      review: {
        ...review
      },
      restaurantName: this.restaurantNames[review.restaurantId],
    }

    const dialogRef = this.dialog.open(SharedModalYesNoComponent, dialogConfig);

    this.subscriptions.push(
      dialogRef.afterClosed().pipe(
        filter(data => data),
        mergeMap(data => this.reviewService.DeleteReview(review.id))
      ).subscribe(
        data => {
          console.log("Dialog output:", data);
          this.reviews = [];
          this.query.pageNumber = 0;
          this.querySubject.next(this.query);
        }
      ));
  }

  isAdmin() {
    return this.userRoles.includes('admin');
  }

  isOwner() {
    return this.userRoles.includes('restaurant_owner');
  }

  edit(review: ReviewViewModel) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      review: {
        ...review
      },
      restaurantName: this.restaurantNames[review.restaurantId],
    }

    const dialogRef = this.dialog.open(SharedEditReviewComponent, dialogConfig);

    this.subscriptions.push(
      dialogRef.afterClosed().subscribe(
        data => {
          console.log("Dialog output:", data);
          this.reviews = [];
          this.query.pageNumber = 0;
          this.querySubject.next(this.query);
        }
      ));
  }

  canEdit(review: ReviewViewModel) {
    return review.createdBy === this.user.sub;
  }
}
