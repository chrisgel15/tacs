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
var TransactionService = /** @class */ (function () {
    function TransactionService(http) {
        this.http = http;
    }
    TransactionService.prototype.CrearTransaccion = function (payload, callbackOk, callbackError) {
        this.http
            .post(config_1.apiTacs + "/user/wallets/" + payload.moneda + "/transactions", payload.data, { observe: 'response' })
            .subscribe(callbackOk, callbackError);
    };
    TransactionService.prototype.GetMyCoins = function (callbackOk, callbackError) {
        this.http
            .get(config_1.apiTacs + "/user/wallets", { observe: 'response' })
            .subscribe(callbackOk, callbackError);
    };
    TransactionService.prototype.GetMyTransactions = function (callback) {
        this.http
            .get(config_1.apiTacs + "/user/transactions")
            .subscribe(callback);
    };
    TransactionService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], TransactionService);
    return TransactionService;
}());
exports.TransactionService = TransactionService;
//# sourceMappingURL=transaction.service.js.map