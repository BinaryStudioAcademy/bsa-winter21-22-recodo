import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VideoUrlService {

  constructor(protected httpClient: HttpClient) { }

  public getFullRequest<TRequest>(
    url: string,
    httpParams?: HttpParams
  ): Observable<HttpResponse<TRequest>> {
    return this.httpClient.get<TRequest>(`${url}`, {
      observe: 'response',
      params: httpParams,
    });
  }
}
