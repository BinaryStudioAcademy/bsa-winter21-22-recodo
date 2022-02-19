import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
  HttpResponse,
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
      .get<T[]>(`${this.APIUrl}}?${params.toString()}`)
      .pipe(catchError(this.handleError));
  }

  getList(): Observable<HttpResponse<T[]>> {
    return this.httpClient
      .get<T[]>(`${this.APIUrl}`, { observe: 'response' })
      .pipe(catchError(this.handleError));
  }

  public getFullRequest<T>(
    httpParams?: any
  ): Observable<HttpResponse<T>> {
    return this.httpClient.get<T>(`${this.APIUrl}`, {
      observe: 'response',
      params: httpParams,
    });
  }

  get(id: string | number): Observable<HttpResponse<T>> {
    return this.httpClient
      .get<T>(`${this.APIUrl}/${id}`, { observe: 'response' })
      .pipe(catchError(this.handleError));
  }

  add<TRequest, TResponse>(
    resource: TRequest
  ): Observable<HttpResponse<TResponse>> {
    return this.httpClient
      .post<TResponse>(`${this.APIUrl}`, resource, { observe: 'response' })
      .pipe(catchError(this.handleError));
  }

  delete(id: string | number): Observable<HttpResponse<T>> {
    return this.httpClient
      .delete<T>(`${this.APIUrl}/${id}`, { observe: 'response' })
      .pipe(catchError(this.handleError));
  }

  update<TRequest, TResponse>(
    resource: TRequest
  ): Observable<HttpResponse<TResponse>> {
    return this.httpClient
      .put<TResponse>(`${this.APIUrl}`, resource, { observe: 'response' })
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    return throwError(() => error);
  }
}
