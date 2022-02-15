import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export abstract class ResourceService<T> {
  private readonly APIUrl = environment.apiUrl + this.getResourceUrl();

  constructor(protected httpClient: HttpClient) {}

  abstract getResourceUrl(): string;

  getListPagination(page: number, count: number): Observable<T[]> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('count', count.toString());

    return this.httpClient
      .get<T[]>(`/${this.APIUrl}}?${params.toString()}`)
      .pipe(catchError(this.handleError));
  }

  getList(): Observable<T[]> {
    return this.httpClient
      .get<T[]>(`/${this.APIUrl}`)
      .pipe(catchError(this.handleError));
  }

  get(id: string | number): Observable<T> {
    return this.httpClient
      .get<T>(`/${this.APIUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  add(resource: T) {
    return this.httpClient
      .post(`/${this.APIUrl}`, resource)
      .pipe(catchError(this.handleError));
  }

  delete(id: string | number) {
    return this.httpClient
      .delete(`/${this.APIUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  update(resource: T) {
    return this.httpClient
      .put(`/${this.APIUrl}`, resource)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    return throwError(() => error);
  }
}
