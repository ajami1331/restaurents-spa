import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedCreateReviewComponent } from './components/shared-create-review/shared-create-review.component';
import {MatDialogModule} from "@angular/material/dialog";
import {MatFormFieldModule} from "@angular/material/form-field";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {MatRadioModule} from "@angular/material/radio";
import {MatButtonModule} from "@angular/material/button";
import {FlexModule} from "@angular/flex-layout";
import {MatInputModule} from "@angular/material/input";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatNativeDateModule} from "@angular/material/core";
import { SharedEditRestaurantComponent } from './components/shared-edit-restaurant/shared-edit-restaurant.component';
import { SharedRestaurantDetailsComponent } from './components/shared-restaurant-details/shared-restaurant-details.component';
import {MatCardModule} from "@angular/material/card";
import {MatIconModule} from "@angular/material/icon";
import {InfiniteScrollModule} from "ngx-infinite-scroll";
import {MatSliderModule} from "@angular/material/slider";
import {MatTableModule} from "@angular/material/table";
import { SharedRestaurentListComponent } from './components/shared-restaurent-list/shared-restaurent-list.component';
import { SharedRestaurentCreateComponent } from './components/shared-restaurent-create/shared-restaurent-create.component';
import { SharedReviewReplyComponent } from './components/shared-review-reply/shared-review-reply.component';
import {RouterModule} from "@angular/router";
import {SharedReviewListComponent} from "./components/shared-review-list/shared-review-list.component";
import { SharedModalYesNoComponent } from './components/shared-modal-yes-no/shared-modal-yes-no.component';
import { SharedEditReviewComponent } from './components/shared-edit-review/shared-edit-review.component';



@NgModule({
  declarations: [SharedCreateReviewComponent, SharedEditRestaurantComponent, SharedRestaurantDetailsComponent, SharedReviewListComponent, SharedRestaurentListComponent, SharedRestaurentCreateComponent, SharedReviewReplyComponent, SharedModalYesNoComponent, SharedEditReviewComponent],
  imports: [
    CommonModule,
    MatDialogModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatProgressBarModule,
    MatRadioModule,
    MatButtonModule,
    FlexModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCardModule,
    MatIconModule,
    InfiniteScrollModule,
    FormsModule,
    MatSliderModule,
    MatTableModule,
    RouterModule,
  ],
  exports: [SharedCreateReviewComponent, SharedEditRestaurantComponent, SharedRestaurantDetailsComponent, SharedReviewListComponent, SharedRestaurentListComponent]
})
export class SharedModule { }
