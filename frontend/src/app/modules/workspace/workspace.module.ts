import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { PersonalPageComponent } from './personal-page/personal-page.component';
import { ContentComponent } from './content/content.component';
import { BaseComponent } from './base/base.component';



@NgModule({
  declarations: [
    NavbarComponent,
    SidebarComponent,
    PersonalPageComponent,
    ContentComponent,
    BaseComponent
  ],
  imports: [
    CommonModule
  ]
})
export class WorkspaceModule { }
