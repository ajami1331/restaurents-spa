import {Component, Inject, OnInit} from '@angular/core';
import {Subscription} from "rxjs";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {tap} from "rxjs/operators";
import {ReviewService} from "../../services/review.service";

@Component({
  selector: 'app-shared-create-review',
  templateUrl: './shared-create-review.component.html',
  styleUrls: ['./shared-create-review.component.scss']
})
export class SharedCreateReviewComponent implements OnInit {
  private createSubscription: Subscription;
  isLoading: boolean;
  form: FormGroup;
  maxDateAllowed: Date;

  constructor(private fb: FormBuilder,
              private dialogRef: MatDialogRef<SharedCreateReviewComponent>,
              @Inject(MAT_DIALOG_DATA) public data,
              private reviewService: ReviewService) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      rating: ['300', [Validators.required]],
      comment: [null, [Validators.required]],
      dateOfVisit: [new Date(), [Validators.required]],
      restaurantId: [this.data.restaurantId],
      restaurantName: [this.data.restaurantName]
    });
    this.maxDateAllowed = new Date();
  }

  close() {
    this.dialogRef.close();
  }

  save() {
    this.createSubscription = this.reviewService.createReview(this.form.getRawValue())
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
