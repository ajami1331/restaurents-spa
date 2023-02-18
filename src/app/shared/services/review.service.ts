import { Injectable } from '@angular/core';
import {catchError, tap} from "rxjs/operators";
import {environment} from "../../../environments/environment";
import {HttpClient, HttpErrorResponse, HttpParams} from "@angular/common/http";
import {Observable, throwError} from "rxjs";
import {PageResponseModel} from "../models/page-response.model";
import {ReviewViewModel} from "../models/review-view.model";

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  constructor(private http: HttpClient) { }

  getReviewByUser(restaurantId: string, userId: string) {
    return this.http.get<any>(API_URL + 'reviews/restaurants/' + restaurantId + '/user/' + userId)
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

  getLatestReview(restaurantId: string) {
    return this.http.get<any>(API_URL + 'reviews/restaurants/' + restaurantId + '/latest/')
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  getHighestRatedReview(restaurantId: string) {
    return this.http.get<any>(API_URL + 'reviews/restaurants/' + restaurantId + '/highest/')
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  getLowestRateReview(restaurantId: string) {
    return this.http.get<any>(API_URL + 'reviews/restaurants/' + restaurantId + '/lowest/')
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  createReview(value) {
    value['rating'] = Math.ceil(parseInt(value['rating']) / 100);
    return this.http.post<any>(API_URL + 'reviews/', value)
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  getReviews(data: any): Observable<PageResponseModel<ReviewViewModel>> {
    const params = new HttpParams()
      .set('pageSize', data.pageSize)
      .set('pageNumber', data.pageNumber);
    return this.http.get<any>(API_URL + 'reviews', {params: params})
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  getPendingReviews(data: any): Observable<PageResponseModel<ReviewViewModel>> {
    const params = new HttpParams()
      .set('pageSize', data.pageSize)
      .set('pageNumber', data.pageNumber);
    return this.http.get<any>(API_URL + 'reviews/pending', {params: params})
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  replyReview(value: any) {
    return this.http.post<any>(API_URL + 'reviews/reply', value)
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  DeleteReview(id: string) {
    return this.http.delete<any>(API_URL + 'reviews/' + id)
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }

  editReview(value: any) {
    value['rating'] = Math.ceil(parseInt(value['rating']) / 100);
    return this.http.put<any>(API_URL + 'reviews/', value)
      .pipe(
        tap((res) => {
          console.log(res);
        }),
        catchError(this.handleError)
      );
  }
}
