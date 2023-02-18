import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Subscription} from "rxjs";
import {RestaurantViewModel} from "../../models/restaurant-view.model";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {RestaurantsService} from "../../services/restaurants.service";
import {tap} from "rxjs/operators";

@Component({
  selector: 'app-shared-edit-restaurant',
  templateUrl: './shared-edit-restaurant.component.html',
  styleUrls: ['./shared-edit-restaurant.component.scss']
})
export class SharedEditRestaurantComponent implements OnInit {
  isLoading: any;
  form: FormGroup;
  private createSubscription: Subscription;
  restaurant: RestaurantViewModel;

  constructor(private fb: FormBuilder,
              private dialogRef: MatDialogRef<SharedEditRestaurantComponent>,
              @Inject(MAT_DIALOG_DATA) public data,
              private restaurantsService: RestaurantsService) { }

  ngOnInit(): void {
    this.restaurant = this.data.restaurant;
    this.form = this.fb.group({
      name: [this.restaurant.name, [Validators.required]],
      id: [this.restaurant.id]
    });
  }

  close() {
    this.dialogRef.close();
  }

  save() {
    this.createSubscription = this.restaurantsService.editRestaurant(this.form.getRawValue())
      .pipe(tap(() => this.isLoading = true))
      .subscribe(res => {
        this.isLoading = false;
        this.dialogRef.close(this.form.value);
      });
  }

  ngOnDestroy(): void {
    if (this.createSubscription)
      this.createSubscription.unsubscribe();
  }
}
