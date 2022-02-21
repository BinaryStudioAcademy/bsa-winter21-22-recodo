import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { PersonalComponent } from './personal/personal.component';
import { BaseComponent } from './base/base.component';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { BaseRoutingModule } from './workspace-routing.module';
import { VideoPageComponent } from './video/video-page/video-page.component';
import { VideoPlayerComponent } from './video/video-player/video-player.component';
import { VideoReactionsComponent } from './video/video-reactions/video-reactions.component';
import { VideoDescriptionComponent } from './video/video-description/video-description.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { VimeModule } from '@vime/angular';

@NgModule({
  declarations: [
    NavbarComponent,
    SidebarComponent,
    PersonalComponent,
    BaseComponent,
    VideoPageComponent,
    VideoPlayerComponent,
    VideoReactionsComponent,
    VideoDescriptionComponent,
  ],
  imports: [
    CommonModule,
    BaseRoutingModule,
    MatButtonToggleModule,
    MatToolbarModule,
    MatSidenavModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    MatCardModule,
    MatInputModule,
    MatListModule,
    SharedModule,
    VimeModule,
  ],
})
export class WorkspaceModule {}
