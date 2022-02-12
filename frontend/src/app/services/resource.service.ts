import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError, map, switchMap, take } from 'rxjs/operators';
import { IResponse } from '../core/models/response';

@Injectable({
  providedIn: 'root',
})
export abstract class ResourceService<T> {
  private readonly APIUrl = environment.apiUrl + this.getResourceUrl();

  constructor(protected httpClient: HttpClient) {}

  abstract getResourceUrl(): string;

  getListPagination(index: number, page: number): Observable<T[]> {
    return this.httpClient.get<IResponse>(`/${this.APIUrl}`).pipe(
      map((item) => JSON.parse(item.data)),
      catchError(this.handleError)
    );
  }

  getList(): Observable<T[]> {
    return this.httpClient.get<IResponse>(`/${this.APIUrl}`).pipe(
      map((item) => JSON.parse(item.data)),
      catchError(this.handleError)
    );
  }

  get(id: string | number): Observable<T> {
    return this.httpClient.get<IResponse>(`/${this.APIUrl}/${id}`).pipe(
      map((item) => JSON.parse(item.data)),
      catchError(this.handleError)
    );
  }

  add(resource: T) {
    return this.httpClient
      .post<T>(`/${this.APIUrl}`, JSON.stringify(resource))
      .pipe(catchError(this.handleError));
  }

  delete(id: string | number) {
    return this.httpClient
      .delete<T>(`/${this.APIUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  update(resource: T) {
    return this.httpClient
      .put<T>(`/${this.APIUrl}`, JSON.stringify(resource))
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    return throwError(() => error);
  }
}
