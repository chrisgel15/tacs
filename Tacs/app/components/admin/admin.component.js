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
var admin_service_1 = require("../../services/admin.service");
var router_1 = require("@angular/router");
var AdminComponent = /** @class */ (function () {
    function AdminComponent(service, router) {
        this.service = service;
        this.router = router;
        document.body.style.background = "linear-gradient(to left, #76b852, #8DC26F)";
    }
    AdminComponent.prototype.ngOnInit = function () {
        $('ul li a').click(function () { $('li a').removeClass("active"); $(this).addClass("active"); });
    };
    AdminComponent.prototype.ngOnDestroy = function () {
        document.body.style.background = "white";
    };
    AdminComponent.prototype.signOut = function () {
        var _this = this;
        this.service.signOut(function () {
            sessionStorage.removeItem('tacs-token');
            _this.router.navigate(['']);
        }, function () {
            alert('Ocurrio un error al desconectarte del sitio.');
        });
    };
    AdminComponent = __decorate([
        core_1.Component({
            selector: 'app-admin',
            templateUrl: './admin.component.html',
            styleUrls: ['./admin.component.css']
        }),
        __metadata("design:paramtypes", [admin_service_1.AdminService, router_1.Router])
    ], AdminComponent);
    return AdminComponent;
}());
exports.AdminComponent = AdminComponent;
//# sourceMappingURL=admin.component.js.map