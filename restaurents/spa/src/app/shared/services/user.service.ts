import { Injectable } from '@angular/core';
import {Observable, throwError} from "rxjs";
import {PageResponseModel} from "../models/page-response.model";
import {HttpClient, HttpErrorResponse, HttpParams} from "@angular/common/http";
import {catchError, tap} from "rxjs/operators";
import {environment} from "../../../environments/environment";
import {UserViewModel} from "../models/user-view.model";

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUsers(data: any): Observable<PageResponseModel<UserViewModel>> {
    const params = new HttpParams()
      .set('pageSize', data.pageSize)
      .set('pageNumber', data.pageNumber);
    return this.http.get<any>(API_URL + 'users', {params: params})
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse): any {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    return throwError(
      'Something bad happened; please try again later.');
  }

  private static log(message: string): any {
    console.log(message);
  }

  DeleteUser(id: string) {
    return this.http.delete<any>(API_URL + 'users/' + id)
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  editUser(value: any) {
    return this.http.put<any>(API_URL + 'users/', value)
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }
}
