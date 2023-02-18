import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import {SharedReviewListComponent} from "../shared/components/shared-review-list/shared-review-list.component";
import {SharedModule} from "../shared/shared.module";

const routes: Routes = [
  { path: '', component: SharedReviewListComponent},
];


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
  ]
})
export class ReviewsModule { }
