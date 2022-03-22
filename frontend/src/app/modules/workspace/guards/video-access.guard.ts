import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { UserDto } from 'src/app/models/user/user-dto';
import { VideoService } from 'src/app/services/video.service';
import { LoginService } from '../../../services/login.service';
import { RegistrationService } from '../../../services/registration.service';

@Injectable({
  providedIn: 'root',
})
export class VideoAccessGuard implements CanActivate {
  private currentUser = {} as UserDto;
  constructor(
    private videoService: VideoService,
    private loginService: LoginService,
    private registrationService: RegistrationService,
    private router: Router
  ) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    this.videoService
      .getVideoById(route.params['videoId'])
      .subscribe((resp) => {
        if (resp.body) {
          if (!resp.body.isPrivate) {
            this.router.navigate(['']);
          }
        }
      });
    return true;
  }
}
