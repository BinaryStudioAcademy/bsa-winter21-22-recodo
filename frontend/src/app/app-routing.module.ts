import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PersonalComponent } from './modules/workspace/personal/personal.component';

const routes: Routes = [
  { path: '', redirectTo: '', pathMatch: 'full' },
  { path:'personal', component:PersonalComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
