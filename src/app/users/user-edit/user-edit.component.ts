import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Subscription} from "rxjs";
import {RestaurantViewModel} from "../../shared/models/restaurant-view.model";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {RestaurantsService} from "../../shared/services/restaurants.service";
import {tap} from "rxjs/operators";
import {UserViewModel} from "../../shared/models/user-view.model";
import {UserService} from "../../shared/services/user.service";

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
  isLoading: any;
  form: FormGroup;
  private createSubscription: Subscription;
  user: UserViewModel;

  constructor(private fb: FormBuilder,
              private dialogRef: MatDialogRef<UserEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data,
              private userService: UserService) { }

  ngOnInit(): void {
    this.user = this.data.user;

    this.form = this.fb.group({
      id: [this.user.id],
      userName: [this.user.userName],
      displayName: [this.user.displayName],
      roles: [this.getRole(this.user.roles)]
    });
    this.form.get('userName').disable();
  }

  close() {
    this.dialogRef.close();
  }

  save() {
    const rawValue ={...this.form.getRawValue()};
    if (this.user.roles.includes(rawValue['roles'])) {
      rawValue.roles = [...this.user.roles];
    }
    this.createSubscription = this.userService.editUser(rawValue)
      .pipe(tap(() => this.isLoading = true))
      .subscribe(res => {
        this.isLoading = false;
        this.dialogRef.close(this.form.value);
      });
  }

  getRole(roles: string[]) {
    if (roles.includes('admin')) {
      return 'admin';
    }
    if (roles.includes('restaurant_owner')) {
      return 'restaurant_owner';
    }
    return 'user';
  }

  ngOnDestroy(): void {
    if (this.createSubscription)
      this.createSubscription.unsubscribe();
  }
}
