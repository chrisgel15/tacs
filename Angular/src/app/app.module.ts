import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { InicioComponent } from './components/inicio/inicio.component';
import { LoginComponent } from './components/inicio/login/login.component';
import { RegistroComponent } from './components/inicio/registro/registro.component';
import { AuthComponent } from './components/auth/auth/auth.component';
import { WalletComponent } from './components/auth/wallet/wallet.component';
import { TransactionComponent } from './components/auth/transaction/transaction.component';
import { AdminComponent } from './components/admin/admin.component';
import { UsersComponent } from './components/admin/users/users.component';

import { InicioService } from './services/inicio.service';
import { TransactionService } from './services/transaction.service';
import { AuthGuardGuard } from './guards/auth-guard.guard';
import { UnauthGuardGuard } from './guards/unauth-guard.guard';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { WalletService } from './services/wallet.service';
import { ReportsComponent } from './components/admin/reports/reports.component';
import { ReportService } from './services/report.service';
import { AdminService } from './services/admin.service';


@NgModule({
  declarations: [
    AppComponent,
    RegistroComponent,
    LoginComponent,
    InicioComponent,
    AuthComponent,
    WalletComponent,
    TransactionComponent,
    AdminComponent,
    ReportsComponent,
    UsersComponent
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
    WalletService,
    ReportService,
    AdminService,
    AuthGuardGuard,
    UnauthGuardGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
