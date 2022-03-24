import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BaseComponent } from './base/base.component';
import { VideoAuthorGuard } from './guards/video-author.guard';
import { PersonalComponent } from './personal/personal.component';
import { SettingsComponent } from './settings/settings.component';
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
        path: 'settings',
        component: SettingsComponent,
      },
      {
        path: '',
        redirectTo: '',
        pathMatch: 'full',
      },
      {
        path: 'video/:id',
        component: VideoPageComponent,
        canActivate: [VideoAuthorGuard],
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
