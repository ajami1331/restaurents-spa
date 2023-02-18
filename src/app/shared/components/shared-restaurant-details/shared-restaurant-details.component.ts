import { Component, OnInit } from '@angular/core';
import {BehaviorSubject, forkJoin, Subscription} from "rxjs";
import {RestaurantViewModel} from "../../models/restaurant-view.model";
import {ReviewViewModel} from "../../models/review-view.model";
import {RestaurantsService} from "../../services/restaurants.service";
import {ActivatedRoute} from "@angular/router";
import {TokenService} from "../../services/token.service";
import {ReviewService} from "../../services/review.service";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {filter, mergeMap, tap} from "rxjs/operators";
import {SharedEditRestaurantComponent} from "../shared-edit-restaurant/shared-edit-restaurant.component";
import {SharedCreateReviewComponent} from "../shared-create-review/shared-create-review.component";
import {SharedModalYesNoComponent} from "../shared-modal-yes-no/shared-modal-yes-no.component";
import {NavService} from "../../services/nav.service";

@Component({
  selector: 'app-shared-restaurant-details',
  templateUrl: './shared-restaurant-details.component.html',
  styleUrls: ['./shared-restaurant-details.component.scss']
})
export class SharedRestaurantDetailsComponent implements OnInit {
  private subscriptions: Subscription[] = [];
  private restaurant: RestaurantViewModel = null;
  isLoading = false;
  private userRoles: Array<string>;
  private reviewedByUser: boolean = false;
  private user: any;
  private restaurantId: string;
  private usersReview: ReviewViewModel;
  private latestReview: ReviewViewModel;
  private highestRatedReview: ReviewViewModel;
  private lowestRatedReview: ReviewViewModel;
  loadData: BehaviorSubject<any> = new BehaviorSubject(true);

  constructor(private restaurantsService: RestaurantsService,
              private route: ActivatedRoute,
              private tokenService: TokenService,
              private reviewService: ReviewService,
              private dialog: MatDialog,
              private navService: NavService) { }

  ngOnInit(): void {
    this.user = this.tokenService.getUser();
    this.userRoles = this.tokenService.getRoles();
    this.restaurantId = this.route.snapshot.paramMap.get('id');
    let obs = [
      this.restaurantsService.getRestaurant(this.restaurantId),
      this.reviewService.getReviewByUser(this.restaurantId, this.user.sub),
      this.reviewService.getLatestReview(this.restaurantId),
      this.reviewService.getHighestRatedReview(this.restaurantId),
      this.reviewService.getLowestRateReview(this.restaurantId),
    ];
    this.subscriptions.push(
      this.loadData.pipe(
        mergeMap(() => forkJoin(obs)),
        tap(() => this.isLoading = true)
      )
        .subscribe(([restaurant, usersReview, latestReview, highestRatedReview, lowestRatedReview]) => {
          this.isLoading = false;
          this.restaurant = restaurant;
          this.reviewedByUser = !!usersReview;
          this.usersReview = usersReview;
          this.latestReview = latestReview;
          this.highestRatedReview = highestRatedReview;
          this.lowestRatedReview = lowestRatedReview;
        })
    );
  }
  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  isAdmin() {
    return this.userRoles.includes('admin');
  }

  isOwner() {
    return this.userRoles.includes('restaurant_owner');
  }

  openEditDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      restaurant: {
        ...this.restaurant
      }
    }

    const dialogRef = this.dialog.open(SharedEditRestaurantComponent, dialogConfig);

    this.subscriptions.push(
      dialogRef.afterClosed().subscribe(
        data => {
          console.log("Dialog output:", data);
          this.loadData.next(true);
        }
      ));
  }

  reviewed() {
    return this.reviewedByUser;
  }

  openReviewDialog() {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      restaurantId: this.restaurantId,
      restaurantName: this.restaurant.name,
    }

    const dialogRef = this.dialog.open(SharedCreateReviewComponent, dialogConfig);

    this.subscriptions.push(
      dialogRef.afterClosed().subscribe(
        data => {
          console.log("Dialog output:", data);
          this.loadData.next(true);
        }
      ));
  }

  openDeleteDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
    }

    const dialogRef = this.dialog.open(SharedModalYesNoComponent, dialogConfig);

    this.subscriptions.push(
      dialogRef.afterClosed().pipe(
        filter(data => data),
        mergeMap(data => this.restaurantsService.DeleteRestaurant(this.restaurant.id))
      ).subscribe(
        data => {
          console.log("Dialog output:", data);
          this.navService.back();
        }
      ));
  }
}
