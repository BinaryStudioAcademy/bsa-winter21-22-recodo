import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TokenExistsGuard } from './guards/token-exists.guard';
import { TokenDoesnotExistGuard } from './guards/token-doesnot-exist.guard';
import { SharedVideoGuard } from './guards/shared-video.guard';
import { SharedVideoPageComponent } from './modules/workspace/video/shared-video/shared-video-page.component';

const routes: Routes = [
  {
    path: 'personal',
    loadChildren: () =>
      import('./modules/workspace/workspace.module').then(
        (m) => m.WorkspaceModule
      ),
    canActivate: [TokenExistsGuard],
  },
  {
    path: '',
    loadChildren: () =>
      import('./modules/auth/auth.module').then((m) => m.AuthModule),
    canActivate: [TokenDoesnotExistGuard],
  },
  {
    path: 'shared/:videoId',
    component: SharedVideoPageComponent,
    canActivate: [TokenExistsGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
