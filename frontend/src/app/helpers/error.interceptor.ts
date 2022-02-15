import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private loginService: LoginService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((response) => {
        console.log('catch error');
        if (response.status === 401) {
          this.router.navigate(['/']);
          this.loginService.logout();
        }

        console.log(response);
        const error = response.error
          ? response.error.error || response.error.message
          : response.message || `${response.status} ${response.statusText}`;

        return throwError(() => error);
      })
    );
  }
}
