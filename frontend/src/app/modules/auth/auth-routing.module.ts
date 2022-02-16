import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component';

import { RegisterPageComponent } from './register-page/register-page.component';


const routes: Routes = [
    {
        path : 'register', component : RegisterPageComponent
    },
    {
        path : 'login', component : LoginPageComponent
    },
    {
        path : '', redirectTo : 'login', pathMatch: 'full'
    }
];

@NgModule({
    imports : [RouterModule.forChild(routes)],
    exports : [RouterModule]
})
export class AuthRoutingModule { }