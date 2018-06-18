import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InicioComponent } from './components/inicio/inicio.component';
import { LoginComponent } from './components/inicio/login/login.component';
import { RegistroComponent } from './components/inicio/registro/registro.component';
import { AuthComponent } from './components/auth/auth/auth.component';
import { WalletComponent } from './components/auth/wallet/wallet.component';
import { TransactionComponent } from './components/auth/transaction/transaction.component';
import { PurchaseComponent } from './components/auth/transaction/purchase/purchase.component';
import { SaleComponent } from './components/auth/transaction/sale/sale.component';

//This is my case 
const routes: Routes = [
    {
        path: '',
        component: InicioComponent,
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
                children: [
                    {
                        path: 'purchase',
                        component: PurchaseComponent
                    },
                    {
                        path: 'sale',
                        component: SaleComponent
                    }
                ]
            }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }