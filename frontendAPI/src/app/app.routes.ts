import { Routes } from '@angular/router';
import { Login } from './components/auth/login/login';

import { ProduitComponent } from './components/produit/produit.component';
import { Register } from './components/auth/register/register';

export const routes: Routes = [
    {
        path : '',
        redirectTo: 'login',
         pathMatch: 'full'
    },
    {
        path: 'login', 
        component: Login
    },
    {
        path : 'register',
        component : Register
    },
    {
        path : 'products',
        component : ProduitComponent
    }


];
