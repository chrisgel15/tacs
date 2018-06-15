import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { RegistroComponent } from './components/inicio/registro/registro.component';
import { LoginComponent } from './components/inicio/login/login.component';
import { InicioComponent } from './components/inicio/inicio.component';
import { AuthComponent } from './components/auth/auth/auth.component';
import { AppRoutingModule } from './app-routing.module';
import { WalletComponent } from './components/auth/wallet/wallet.component';
import { TransactionComponent } from './components/auth/transaction/transaction.component';



@NgModule({
  declarations: [
    AppComponent,
    RegistroComponent,
    LoginComponent,
    InicioComponent,
    AuthComponent,
    WalletComponent,
    TransactionComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
