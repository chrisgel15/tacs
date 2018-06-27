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
var wallet_service_1 = require("../../../services/wallet.service");
var WalletComponent = /** @class */ (function () {
    function WalletComponent(walletService) {
        this.walletService = walletService;
    }
    WalletComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.walletService.getWallet()
            .subscribe(function (wallets) {
            var filteredWallets = wallets.filter(function (wallet) { return wallet.Balance > 0; });
            _this.wallets = filteredWallets;
        });
    };
    WalletComponent.prototype.balance = function () {
        return this.wallets.reduce(function (total, wallet) {
            return total + (wallet.Cotizacion * wallet.Balance);
        }, 0).toFixed(2);
    };
    WalletComponent = __decorate([
        core_1.Component({
            selector: 'app-wallet',
            templateUrl: './wallet.component.html',
            styleUrls: ['./wallet.component.css']
        }),
        __metadata("design:paramtypes", [wallet_service_1.WalletService])
    ], WalletComponent);
    return WalletComponent;
}());
exports.WalletComponent = WalletComponent;
//# sourceMappingURL=wallet.component.js.map