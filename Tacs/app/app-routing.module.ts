import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InicioComponent } from './components/inicio/inicio.component';
import { LoginComponent } from './components/inicio/login/login.component';
import { RegistroComponent } from './components/inicio/registro/registro.component';
import { AuthComponent } from './components/auth/auth/auth.component';
import { WalletComponent } from './components/auth/wallet/wallet.component';
import { TransactionComponent } from './components/auth/transaction/transaction.component';
import { AuthGuardGuard } from './guards/auth-guard.guard';
import { UnauthGuardGuard } from './guards/unauth-guard.guard';
import { AdminComponent } from './components/admin/admin.component';
import { ReportsComponent } from './components/admin/reports/reports.component';
import { UsersComponent } from './components/admin/users/users.component';

//This is my case 
const routes: Routes = [
    {
        path: '',
        component: InicioComponent,
        canActivate: [UnauthGuardGuard],
        children: [
            {
                path: '',
                component: LoginComponent
            },
            {
                path: 'registro',
                component: RegistroComponent
            }
        ]
    },
    {
        path: 'auth',
        component: AuthComponent,
        canActivate: [AuthGuardGuard],
        children: [
            {
                path: '',
                redirectTo: 'wallet',
                pathMatch: 'full'
            },
            {
                path: 'wallet',
                component: WalletComponent
            },
            {
                path: 'transaction',
                component: TransactionComponent,
            },
            {
                path: 'reports',
                component: ReportsComponent
            },
            {
                path: 'users',
                component: UsersComponent
            }   
        ]
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }