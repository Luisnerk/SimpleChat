import { Routes } from '@angular/router';
import { Main } from './main/main';
import { LoginScreen } from './login-screen/login-screen';
import { authGuard } from './_guards/auth-guard';
import { main } from '@popperjs/core';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
    },
    {
        path: 'login', 
        component: LoginScreen
    },
    {

        canActivate: [authGuard],
        path: 'main',
        children: [
            { path: "", component: Main }
        ]
    }
];
