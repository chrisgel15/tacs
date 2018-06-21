import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';

import { AppComponent } from './app.component';
import { RegistroComponent } from './components/inicio/registro/registro.component';
import { LoginComponent } from './components/inicio/login/login.component';
import { InicioComponent } from './components/inicio/inicio.component';
import { AuthComponent } from './components/auth/auth/auth.component';
import { AppRoutingModule } from './app-routing.module';
import { WalletComponent } from './components/auth/wallet/wallet.component';
import { TransactionComponent } from './components/auth/transaction/transaction.component';

import { Authorization } from './services/authorization'
import { InicioService } from './services/inicio.service';
import { TransactionService } from './services/transaction.service';


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
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    InicioService,
    TransactionService,
    Authorization
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
