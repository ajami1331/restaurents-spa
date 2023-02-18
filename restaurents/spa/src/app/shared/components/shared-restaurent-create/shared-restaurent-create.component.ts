import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Subscription} from "rxjs";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {RestaurantsService} from "../../services/restaurants.service";
import {tap} from "rxjs/operators";

@Component({
  selector: 'app-shared-restaurent-create',
  templateUrl: './shared-restaurent-create.component.html',
  styleUrls: ['./shared-restaurent-create.component.scss']
})
export class SharedRestaurentCreateComponent implements OnInit {
  isLoading: any;
  form: FormGroup;
  private createSubscription: Subscription;

  constructor(private fb: FormBuilder,
              private dialogRef: MatDialogRef<SharedRestaurentCreateComponent>,
              @Inject(MAT_DIALOG_DATA) public data,
              private restaurantsService: RestaurantsService) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      name: [null, [Validators.required]],
    });
  }

  close() {
    this.dialogRef.close();
  }

  save() {
    this.createSubscription = this.restaurantsService.createRestaurant(this.form.getRawValue())
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
