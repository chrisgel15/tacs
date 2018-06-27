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
var http_1 = require("@angular/common/http");
var config_1 = require("../config");
var AdminService = /** @class */ (function () {
    function AdminService(http) {
        this.http = http;
    }
    AdminService.prototype.getUsers = function (callback) {
        this.http.get(config_1.apiTacs + '/admin/users').subscribe(callback);
    };
    AdminService.prototype.compareUsers = function (user1, user2, callbackOk, callbackError) {
        var params = new http_1.HttpParams().set('userName1', user1).set('userName2', user2);
        this.http
            .get(config_1.apiTacs + '/admin/compare', { params: params, observe: 'response' })
            .subscribe(callbackOk, callbackError);
    };
    AdminService.prototype.signOut = function (callbackOk, callbackError) {
        this.http
            .delete(config_1.apiTacs + '/token', { observe: 'response' })
            .subscribe(function (res) {
            if (res.status === 200) {
                callbackOk();
            }
            else {
                callbackError();
            }
        }, function (err) { return callbackError(); });
    };
    AdminService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], AdminService);
    return AdminService;
}());
exports.AdminService = AdminService;
//# sourceMappingURL=admin.service.js.map