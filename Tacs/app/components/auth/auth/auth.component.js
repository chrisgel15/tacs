"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var inicio_service_1 = require("../../../services/inicio.service");
var AuthComponent = /** @class */ (function () {
    function AuthComponent(router, service) {
        this.router = router;
        this.service = service;
    }
    AuthComponent.prototype.ngOnInit = function () {
        this.isAdmin = (sessionStorage.getItem('admin') == 'SI');
    };
    AuthComponent.prototype.signOut = function () {
        var _this = this;
        this.service.signOut(function () {
            sessionStorage.removeItem('tacs-token');
            _this.router.navigate(['']);
        }, function () {
            alert('Ocurrio un error al desconectarte del sitio.');
        });
    };
    AuthComponent = __decorate([
        core_1.Component({
            selector: 'app-auth',
            templateUrl: './auth.component.html',
            styleUrls: ['./auth.component.css']
        }),
        __metadata("design:paramtypes", [router_1.Router, inicio_service_1.InicioService])
    ], AuthComponent);
    return AuthComponent;
}());
exports.AuthComponent = AuthComponent;
//# sourceMappingURL=auth.component.js.map