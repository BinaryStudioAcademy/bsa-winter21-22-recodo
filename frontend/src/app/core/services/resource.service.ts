import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { IResponse } from '../models/response';

@Injectable({
  providedIn: 'root',
})
export abstract class ResourceService<T> {
  private readonly APIUrl = environment.apiUrl + this.getResourceUrl();

  constructor(protected httpClient: HttpClient) {}

  abstract getResourceUrl(): string;

  toServerModel(entity: T): string {
    return JSON.stringify(entity);
  }

  fromServerModel(json: IResponse): T[] {
    var data: T[] = Array.isArray(json.data) ? json.data : [json.data];
    return data;
  }

  getList(index: number, page: number): Observable<T[]> {
    let params = new HttpParams()
      .set('limit', index.toString())
      .set('offset', page.toString());

    return this.httpClient
      .get<IResponse[]>(`/${this.APIUrl}?${params.toString()}`)
      .pipe(
        switchMap((list) => list.map((item) => this.fromServerModel(item))),
        catchError(this.handleError)
      );
  }

  get(id: string | number): Observable<T> {
    return this.httpClient.get<IResponse>(`/${this.APIUrl}/${id}`).pipe(
      switchMap((item) => this.fromServerModel(item)),
      catchError(this.handleError)
    );
  }

  add(resource: T): Observable<IResponse> {
    return this.httpClient
      .post<IResponse>(`/${this.APIUrl}`, this.toServerModel(resource))
      .pipe(catchError(this.handleError));
  }

  delete(id: string | number): Observable<IResponse> {
    return this.httpClient
      .delete<IResponse>(`/${this.APIUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  update(resource: T) {
    return this.httpClient
      .put(`/${this.APIUrl}`, this.toServerModel(resource))
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    return throwError(() => error);
  }
}
