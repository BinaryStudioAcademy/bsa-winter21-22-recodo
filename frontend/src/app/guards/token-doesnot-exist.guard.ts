import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { LoginService } from '../services/login.service';

@Injectable({
  providedIn: 'root'
})
export class TokenDoesnotExistGuard implements CanActivate {
  constructor(
    private loginService : LoginService,
    private router : Router
  ) { }

  canActivate(route: ActivatedRouteSnapshot) : boolean {
    if(this.loginService.areTokensExist() ) {
      let redirect_url = route.queryParams['redirect_url'];
      if (redirect_url) {
        window.location.href= `${redirect_url}?access_token=${localStorage.getItem('accessToken')}`;
      }
      else {
        this.router.navigate(['/personal']);
      }
    }
    return !this.loginService.areTokensExist();
  }
}
