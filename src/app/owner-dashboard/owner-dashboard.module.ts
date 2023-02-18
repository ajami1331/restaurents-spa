import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import {RouterModule, Routes} from "@angular/router";
import {MatCardModule} from "@angular/material/card";
import {MatIconModule} from "@angular/material/icon";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {FlexModule} from "@angular/flex-layout";
import {InfiniteScrollModule} from "ngx-infinite-scroll";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatSliderModule} from "@angular/material/slider";
import {MatDialogModule} from "@angular/material/dialog";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatButtonModule} from "@angular/material/button";
import {MatTableModule} from "@angular/material/table";
import {MatRadioModule} from "@angular/material/radio";
import {MatInputModule} from "@angular/material/input";
import {SharedRestaurantDetailsComponent} from "../shared/components/shared-restaurant-details/shared-restaurant-details.component";
import {SharedModule} from "../shared/shared.module";

const routes: Routes = [
  { path: '', component: DashboardComponent},
  { path: ':id', component: SharedRestaurantDetailsComponent},
];

@NgModule({
  declarations: [DashboardComponent],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatProgressBarModule,
    FlexModule,
    InfiniteScrollModule,
    FormsModule,
    MatSliderModule,
    MatDialogModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatTableModule,
    MatRadioModule,
    MatInputModule,
    SharedModule
  ]
})
export class OwnerDashboardModule { }
