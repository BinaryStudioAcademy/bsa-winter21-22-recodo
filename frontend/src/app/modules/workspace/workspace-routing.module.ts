import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BaseComponent } from './base/base.component';
import { PersonalComponent } from './personal/personal.component';

const routes: Routes = [
  { path: '', component: BaseComponent, children: [
      {
        path: '', component:PersonalComponent
      },
      {
        path: '', redirectTo: '', pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BaseRoutingModule { }