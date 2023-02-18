import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import {MatTableModule} from "@angular/material/table";
import {FlexModule} from "@angular/flex-layout";
import {MatSliderModule} from "@angular/material/slider";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import {MatCardModule} from "@angular/material/card";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import {MatDialogModule} from "@angular/material/dialog";
import {MatRadioModule} from "@angular/material/radio";
import {MatFormFieldModule} from "@angular/material/form-field";
import {SharedModule} from "../shared/shared.module";
import {SharedRestaurantDetailsComponent} from "../shared/components/shared-restaurant-details/shared-restaurant-details.component";
import {SharedRestaurentListComponent} from "../shared/components/shared-restaurent-list/shared-restaurent-list.component";

const routes: Routes = [
  { path: '', component: SharedRestaurentListComponent},
  { path: ':id', component: SharedRestaurantDetailsComponent},
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatTableModule,
    FlexModule,
    MatSliderModule,
    FormsModule,
    InfiniteScrollModule,
    MatCardModule,
    MatProgressBarModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatRadioModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    SharedModule
  ],
  exports: []
})
export class RestaurantsModule { }
