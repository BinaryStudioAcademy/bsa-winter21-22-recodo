import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { VideoDto } from 'src/app/models/video/video-dto';
import { VideoService } from 'src/app/services/video.service';
import { LoginService } from '../services/login.service';

@Injectable({
  providedIn: 'root',
})
export class VideoAccessGuard implements CanActivate {
  private currentVideo = {} as VideoDto;
  constructor(
    private videoService: VideoService,
    private loginService: LoginService,
    private router: Router
  ) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    this.videoService
      .getVideoById(route.params['videoId'])
      .subscribe((resp) => {
        if (resp.body) {
          this.currentVideo = resp.body;
        }
      });
    if (this.currentVideo.isPrivate) {
      this.router.navigate(['/personal']);
    }
    return !this.loginService.areTokensExist();
  }
}
