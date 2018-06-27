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
var BehaviorSubject_1 = require("rxjs/BehaviorSubject");
var config_1 = require("../config");
var InicioService = /** @class */ (function () {
    function InicioService(http) {
        this.http = http;
        this.EmitirErrorSubject = new BehaviorSubject_1.BehaviorSubject({ isError: false, msg: null });
        this.InfoInicio = this.EmitirErrorSubject.asObservable();
        //this.EmitirError$ = this.EmitirErrorSubject.asObservable();
    }
    InicioService.prototype.IniciarSesion = function (data, callback) {
        this.http.post(config_1.apiTacs + '/token', data, { observe: 'response' })
            .subscribe(function (resp) { return callback(resp); }, function (err) { return callback(err); });
    };
    InicioService.prototype.Registrar = function (data, callback) {
        this.http.post(config_1.apiTacs + '/user', data, { observe: 'response' })
            .subscribe(function (resp) { return callback(resp); }, function (err) { return callback(err); });
    };
    InicioService.prototype.InfoDelCliente = function (callbackOk, callbackError) {
        this.http.get(config_1.apiTacs + '/user', { observe: 'response' })
            .subscribe(function (res) { return callbackOk(res.body); }, function (err) { return callbackError(err); });
    };
    InicioService.prototype.signOut = function (callbackOk, callbackError) {
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
    InicioService.prototype.EmitirError = function (data) {
        this.EmitirErrorSubject.next(data);
    };
    InicioService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], InicioService);
    return InicioService;
}());
exports.InicioService = InicioService;
//# sourceMappingURL=inicio.service.js.map