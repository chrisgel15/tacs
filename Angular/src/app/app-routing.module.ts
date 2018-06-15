import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InicioComponent } from './components/inicio/inicio.component';
import { AuthComponent } from './components/auth/auth/auth.component';
import { WalletComponent } from './components/auth/wallet/wallet.component';
import { TransactionComponent } from './components/auth/transaction/transaction.component';

//This is my case 
const routes: Routes = [
    {
        path: '',
        component: InicioComponent
    },
    {
        path: 'auth',
        component: AuthComponent,
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
                component: TransactionComponent
            }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }