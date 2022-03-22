import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BaseComponent } from './base/base.component';
import { VideoAccessGuard } from './guards/video-access.guard';
import { VideoAuthorGuard } from './guards/video-author.guard';
import { PersonalComponent } from './personal/personal.component';
import { SharedVideoPageComponent } from './video/shared-video/shared-video-page.component';
import { VideoPageComponent } from './video/video-page/video-page.component';

const routes: Routes = [
  {
    path: '',
    component: BaseComponent,
    children: [
      {
        path: '',
        component: PersonalComponent,
      },
      {
        path: '',
        redirectTo: '',
        pathMatch: 'full',
      },
      {
        path: 'video/:videoId',
        component: VideoPageComponent,
        canActivate: [VideoAuthorGuard],
      },
      {
        path: 'video/:videoId/shared',
        component: SharedVideoPageComponent,
        canActivate: [VideoAccessGuard],
      },
      {
        path: ':id',
        component: PersonalComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BaseRoutingModule {}
