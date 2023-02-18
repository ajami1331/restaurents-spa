import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpParams} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {catchError, tap} from "rxjs/operators";
import {Observable, throwError} from "rxjs";
import {RestaurantViewModel} from "../models/restaurant-view.model";
import {PageResponseModel} from "../models/page-response.model";

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class RestaurantsService {

  constructor(private http: HttpClient) { }

  getRestaurants(data) : Observable<PageResponseModel<RestaurantViewModel>> {
    const params = new HttpParams()
                          .set('pageSize', data.pageSize)
                          .set('pageNumber', data.pageNumber)
                          .set('lowerBound', data.lowerBound)
                          .set('upperBound', data.upperBound);
    return this.http.get<any>(API_URL + 'restaurants', {params: params})
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

  getRestaurant(id: string): Observable<RestaurantViewModel> {
    return this.http.get<any>(API_URL + 'restaurants/' + id)
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  getRestaurantName(data: string) : Observable<string> {
    return this.http.get<any>(API_URL + 'restaurants/' + data + '/name')
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  createRestaurant(value: any) {
    return this.http.post<any>(API_URL + 'restaurants/', value)
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  editRestaurant(value: any) {
    return this.http.put<any>(API_URL + 'restaurants/', value)
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  DeleteRestaurant(id: string) {
    return this.http.delete<any>(API_URL + 'restaurants/' + id)
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }
}
