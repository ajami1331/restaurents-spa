import { Component, OnInit } from '@angular/core';
import {ReviewViewModel} from "../../shared/models/review-view.model";
import {BehaviorSubject, forkJoin, of, Subscription} from "rxjs";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {TokenService} from "../../shared/services/token.service";
import {filter, mergeMap, tap} from "rxjs/operators";;
import {SharedModalYesNoComponent} from "../../shared/components/shared-modal-yes-no/shared-modal-yes-no.component";
import {UserService} from "../../shared/services/user.service";
import {UserViewModel} from "../../shared/models/user-view.model";
import {UserEditComponent} from "../user-edit/user-edit.component";

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  query = {
    pageSize: 12,
    pageNumber: 0,
  }
  querySubject: BehaviorSubject<any> = new BehaviorSubject(this.query);
  distance: number = 3;
  throttle: number = 400;
  isLoadingResults: boolean = false;
  pageCount = 10;
  private subscriptions: Subscription[] = [];
  private totalCount: any;
  loggedInUser: any;
  private userRoles: string[];
  users: UserViewModel[] = [];

  constructor(private userService: UserService,
              private dialog: MatDialog,
              private tokenService: TokenService) { }

  ngOnInit(): void {
    this.loggedInUser = this.tokenService.getUser();
    this.userRoles = this.tokenService.getRoles();
    this.subscriptions.push(
      this.querySubject.pipe(
        tap(() => this.isLoadingResults = true),
        mergeMap(data => this.userService.getUsers(data))
      ).subscribe((res) => {
        this.users.push(...res.data);
        this.pageCount = res.pageCount;
        this.query.pageNumber = res.pageNumber;
        this.query.pageSize = res.pageSize;
        this.totalCount = res.totalCount;
        this.isLoadingResults = false;
      }));
  }
  onScroll() {
    if (this.isLoadingResults) {
      return;
    }
    if (this.query.pageNumber + 1 >= this.pageCount) {
      return;
    }
    this.query.pageNumber++;
    this.querySubject.next(this.query);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }


  delete(user: UserViewModel) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {

    }

    const dialogRef = this.dialog.open(SharedModalYesNoComponent, dialogConfig);

    this.subscriptions.push(
      dialogRef.afterClosed().pipe(
        filter(data => data),
        mergeMap(data => this.userService.DeleteUser(user.id))
      ).subscribe(
        data => {
          console.log("Dialog output:", data);
          this.users = [];
          this.query.pageNumber = 0;
          this.querySubject.next(this.query);
        }
      ));
  }

  edit(user: UserViewModel) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      user: {
        ...user
      },
    }

    const dialogRef = this.dialog.open(UserEditComponent, dialogConfig);

    this.subscriptions.push(
      dialogRef.afterClosed().subscribe(
        data => {
          console.log("Dialog output:", data);
          this.users = [];
          this.query.pageNumber = 0;
          this.querySubject.next(this.query);
        }
      ));
  }
}
