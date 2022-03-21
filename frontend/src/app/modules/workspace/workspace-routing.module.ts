import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BaseComponent } from './base/base.component';
import { InviteFinishComponent } from './invite-finish/invite-finish.component';
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
      { path: 'inviteteam/:email', component: InviteFinishComponent },
      {
        path: '',
        redirectTo: '',
        pathMatch: 'full',
      },
      {
        path: 'video',
        component: VideoPageComponent,
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
