import { Injectable } from '@angular/core';
import { ActivatedRoute, CanActivate, Router } from '@angular/router';
import { LoginService } from '../services/login.service';

@Injectable({
  providedIn: 'root',
})
export class SharedVideoGuard implements CanActivate {
  constructor(
    private loginService: LoginService,
    private router: Router,
    private route: ActivatedRoute
  ) {}
  canActivate(): boolean {
    if (!this.loginService.areTokensExist()) {
      this.route.queryParams.subscribe((params) => {
        localStorage.setItem('videoId', params['videoId']);
      });
      this.router.navigate(['/login']);
    }
    return this.loginService.areTokensExist();
  }
}
