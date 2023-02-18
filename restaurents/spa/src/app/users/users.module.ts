import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import {RouterModule, Routes} from "@angular/router";
import {InfiniteScrollModule} from "ngx-infinite-scroll";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {MatIconModule} from "@angular/material/icon";
import {MatCardModule} from "@angular/material/card";
import {MatDialogModule} from "@angular/material/dialog";
import {MatButtonModule} from "@angular/material/button";
import {FlexModule} from "@angular/flex-layout";
import {MatTableModule} from "@angular/material/table";
import {MatSliderModule} from "@angular/material/slider";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatRadioModule} from "@angular/material/radio";
import {MatFormFieldModule} from "@angular/material/form-field";
import {SharedModule} from "../shared/shared.module";
import {MatInputModule} from "@angular/material/input";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatNativeDateModule} from "@angular/material/core";

const routes: Routes = [
  { path: '', component: UserListComponent},
];

@NgModule({
  declarations: [UserListComponent, UserEditComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
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
    SharedModule
  ]
})
export class UsersModule { }
