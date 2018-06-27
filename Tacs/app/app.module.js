"use strict";
//import { NgModule }      from '@angular/core';
//import { BrowserModule } from '@angular/platform-browser';
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
//import { AppComponent }  from './app.component';
//@NgModule({
//  imports:      [ BrowserModule ],
//  declarations: [ AppComponent ],
//  bootstrap:    [ AppComponent ]
//})
//export class AppModule { }
var platform_browser_1 = require("@angular/platform-browser");
var core_1 = require("@angular/core");
var animations_1 = require("@angular/platform-browser/animations");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/common/http");
var app_routing_module_1 = require("./app-routing.module");
var app_component_1 = require("./app.component");
var inicio_component_1 = require("./components/inicio/inicio.component");
var login_component_1 = require("./components/inicio/login/login.component");
var registro_component_1 = require("./components/inicio/registro/registro.component");
var auth_component_1 = require("./components/auth/auth/auth.component");
var wallet_component_1 = require("./components/auth/wallet/wallet.component");
var transaction_component_1 = require("./components/auth/transaction/transaction.component");
var admin_component_1 = require("./components/admin/admin.component");
var users_component_1 = require("./components/admin/users/users.component");
var inicio_service_1 = require("./services/inicio.service");
var transaction_service_1 = require("./services/transaction.service");
var auth_guard_guard_1 = require("./guards/auth-guard.guard");
var unauth_guard_guard_1 = require("./guards/unauth-guard.guard");
var token_interceptor_service_1 = require("./services/token-interceptor.service");
var wallet_service_1 = require("./services/wallet.service");
var reports_component_1 = require("./components/admin/reports/reports.component");
var report_service_1 = require("./services/report.service");
var admin_service_1 = require("./services/admin.service");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            declarations: [
                app_component_1.AppComponent,
                registro_component_1.RegistroComponent,
                login_component_1.LoginComponent,
                inicio_component_1.InicioComponent,
                auth_component_1.AuthComponent,
                wallet_component_1.WalletComponent,
                transaction_component_1.TransactionComponent,
                admin_component_1.AdminComponent,
                reports_component_1.ReportsComponent,
                users_component_1.UsersComponent
            ],
            imports: [
                platform_browser_1.BrowserModule,
                animations_1.BrowserAnimationsModule,
                forms_1.FormsModule,
                app_routing_module_1.AppRoutingModule,
                http_1.HttpClientModule
            ],
            providers: [
                inicio_service_1.InicioService,
                transaction_service_1.TransactionService,
                wallet_service_1.WalletService,
                report_service_1.ReportService,
                admin_service_1.AdminService,
                auth_guard_guard_1.AuthGuardGuard,
                unauth_guard_guard_1.UnauthGuardGuard,
                {
                    provide: http_1.HTTP_INTERCEPTORS,
                    useClass: token_interceptor_service_1.TokenInterceptorService,
                    multi: true
                }
            ],
            bootstrap: [app_component_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map