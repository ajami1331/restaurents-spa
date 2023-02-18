import {Component, Inject, OnInit} from '@angular/core';
import {Subscription} from "rxjs";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ReviewViewModel} from "../../models/review-view.model";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ReviewService} from "../../services/review.service";
import {tap} from "rxjs/operators";

@Component({
  selector: 'app-shared-review-reply',
  templateUrl: './shared-review-reply.component.html',
  styleUrls: ['./shared-review-reply.component.scss']
})
export class SharedReviewReplyComponent implements OnInit {
  private replySubscription: Subscription;
  isLoading: boolean;
  form: FormGroup;
  review: ReviewViewModel;

  constructor(private fb: FormBuilder,
              private dialogRef: MatDialogRef<SharedReviewReplyComponent>,
              @Inject(MAT_DIALOG_DATA) public data,
              private reviewService: ReviewService) { }

  ngOnInit(): void {
    this.review = this.data.review;
    this.form = this.fb.group({
      reply: [this.review.reply, [Validators.required]],
      id: [this.review.id],
    });
  }

  close() {
    this.dialogRef.close();
  }

  save() {
    this.replySubscription = this.reviewService.replyReview(this.form.getRawValue())
      .pipe(tap(() => this.isLoading = true))
      .subscribe(res => {
        this.isLoading = false;
        this.dialogRef.close(this.form.value);
      });
  }

  ngOnDestroy(): void {
    if (this.replySubscription)
      this.replySubscription.unsubscribe();
  }

  getStars(rating: number) {
    return Array(rating);
  }
}
