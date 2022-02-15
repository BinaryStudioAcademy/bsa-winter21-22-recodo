import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { LoginService } from '../services/login.service';

@Injectable({
  providedIn: 'root'
})
export class TokenDoesnotExistGuard implements CanActivate {
  constructor(
    private loginService : LoginService,
    private router : Router
  ) { }

  canActivate( ) : boolean {
    if(this.loginService.areTokensExist()) {
      this.router.navigate(['/me']);
    }
    return !this.loginService.areTokensExist();
  }
}
