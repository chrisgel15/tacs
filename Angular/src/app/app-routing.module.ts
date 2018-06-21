import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InicioComponent } from './components/inicio/inicio.component';
import { LoginComponent } from './components/inicio/login/login.component';
import { RegistroComponent } from './components/inicio/registro/registro.component';
import { AuthComponent } from './components/auth/auth/auth.component';
import { WalletComponent } from './components/auth/wallet/wallet.component';
import { TransactionComponent } from './components/auth/transaction/transaction.component';
import { AuthGuardGuard } from './guards/auth-guard.guard';
import { UnauthGuardGuard } from './guards/unauth-guard.guard';

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
            }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }