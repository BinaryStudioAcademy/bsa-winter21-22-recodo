import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BaseComponent } from './base/base.component';
import { PersonalComponent } from './personal/personal.component';
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
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BaseRoutingModule {}
