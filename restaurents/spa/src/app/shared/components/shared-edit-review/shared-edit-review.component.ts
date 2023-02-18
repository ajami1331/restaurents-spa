import {Component, Inject, OnInit} from '@angular/core';
import {Subscription} from "rxjs";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ReviewService} from "../../services/review.service";
import {tap} from "rxjs/operators";
import {ReviewViewModel} from "../../models/review-view.model";

@Component({
  selector: 'app-shared-edit-review',
  templateUrl: './shared-edit-review.component.html',
  styleUrls: ['./shared-edit-review.component.scss']
})
export class SharedEditReviewComponent implements OnInit {
  private createSubscription: Subscription;
  isLoading: boolean;
  form: FormGroup;
  maxDateAllowed: Date;
  private review: ReviewViewModel;

  constructor(private fb: FormBuilder,
              private dialogRef: MatDialogRef<SharedEditReviewComponent>,
              @Inject(MAT_DIALOG_DATA) public data,
              private reviewService: ReviewService) { }

  ngOnInit(): void {
    this.review = this.data.review;
    this.form = this.fb.group({
      rating: [(this.review.rating * 100).toString(10), [Validators.required]],
      comment: [this.review.comment, [Validators.required]],
      dateOfVisit: [this.review.dateOfVisit, [Validators.required]],
      id: [this.review.id]
    });
    this.maxDateAllowed = new Date();
  }

  close() {
    this.dialogRef.close();
  }

  save() {
    this.createSubscription = this.reviewService.editReview(this.form.getRawValue())
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
