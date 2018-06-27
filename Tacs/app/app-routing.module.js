"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var inicio_component_1 = require("./components/inicio/inicio.component");
var login_component_1 = require("./components/inicio/login/login.component");
var registro_component_1 = require("./components/inicio/registro/registro.component");
var auth_component_1 = require("./components/auth/auth/auth.component");
var wallet_component_1 = require("./components/auth/wallet/wallet.component");
var transaction_component_1 = require("./components/auth/transaction/transaction.component");
var auth_guard_guard_1 = require("./guards/auth-guard.guard");
var unauth_guard_guard_1 = require("./guards/unauth-guard.guard");
var reports_component_1 = require("./components/admin/reports/reports.component");
var users_component_1 = require("./components/admin/users/users.component");
//This is my case 
var routes = [
    {
        path: '',
        component: inicio_component_1.InicioComponent,
        canActivate: [unauth_guard_guard_1.UnauthGuardGuard],
        children: [
            {
                path: '',
                component: login_component_1.LoginComponent
            },
            {
                path: 'registro',
                component: registro_component_1.RegistroComponent
            }
        ]
    },
    {
        path: 'auth',
        component: auth_component_1.AuthComponent,
        canActivate: [auth_guard_guard_1.AuthGuardGuard],
        children: [
            {
                path: '',
                redirectTo: 'wallet',
                pathMatch: 'full'
            },
            {
                path: 'wallet',
                component: wallet_component_1.WalletComponent
            },
            {
                path: 'transaction',
                component: transaction_component_1.TransactionComponent,
            },
            {
                path: 'reports',
                component: reports_component_1.ReportsComponent
            },
            {
                path: 'users',
                component: users_component_1.UsersComponent
            }
        ]
    }
];
var AppRoutingModule = /** @class */ (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forRoot(routes)],
            exports: [router_1.RouterModule]
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());
exports.AppRoutingModule = AppRoutingModule;
//# sourceMappingURL=app-routing.module.js.map