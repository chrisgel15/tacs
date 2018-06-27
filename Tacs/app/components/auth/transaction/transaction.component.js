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
var transaction_service_1 = require("./../../../services/transaction.service");
var config_1 = require("../../../config");
// const coinmarket = 'https://api.coinmarketcap.com/v1';
var TransactionComponent = /** @class */ (function () {
    function TransactionComponent(http, transac) {
        this.http = http;
        this.transac = transac;
        this.MisTransacciones = [];
        this.carga = {
            itemsCompra: true,
            itemsVenta: true,
            itemsTrans: true
        };
        this.compra = {
            procesando: false,
            habilitado: false,
            cantidad: null,
            precio: null,
            moneda: null
        };
        this.venta = {
            procesando: false,
            habilitado: false,
            cantidad: null,
            cantidadMax: null,
            precio: null,
            moneda: null
        };
        this.modal = { tipo: '', titulo: '', mensaje: '' };
        document.body.style.background = "linear-gradient(to left, #76b852, #8DC26F)";
    }
    TransactionComponent.prototype.ngOnInit = function () {
        this.TraerDatosMonedas();
    };
    TransactionComponent.prototype.ngOnDestroy = function () {
        document.body.style.background = "white";
    };
    TransactionComponent.prototype.getDatosCompra = function (moneda) {
        this.compra.habilitado = true;
        var coin = this.coins.find(function (c) { return c.name === moneda; });
        this.compra.precio = coin.price;
        this.compra.cantidad = 1;
        this.compra.moneda = moneda;
    };
    TransactionComponent.prototype.getDatosVenta = function (moneda) {
        this.venta.habilitado = true;
        this.venta.cantidadMax = this.MisMonedas.find(function (c) { return c.name === moneda; }).amount;
        this.venta.precio = this.round10(this.coins.find(function (c) { return c.name === moneda; }).price * this.venta.cantidadMax, -3);
        this.venta.moneda = moneda;
        this.venta.cantidad = this.round10(this.venta.cantidadMax, -5);
    };
    TransactionComponent.prototype.CompraOnKeyUpPrecio = function () {
        var _this = this;
        var price = this.coins.find(function (c) { return c.name === _this.compra.moneda; }).price;
        this.compra.precio = this.round10(this.compra.precio, -3);
        this.compra.cantidad = this.round10(this.compra.precio / parseFloat(price.toString()), -5);
    };
    TransactionComponent.prototype.CompraOnKeyUpCantidad = function () {
        var _this = this;
        var price = this.coins.find(function (c) { return c.name === _this.compra.moneda; }).price;
        this.compra.cantidad = this.round10(this.compra.cantidad, -5);
        this.compra.precio = this.round10(this.compra.cantidad * price, -3);
    };
    TransactionComponent.prototype.VentaOnKeyUpPrecio = function () {
        var _this = this;
        var price = this.coins.find(function (c) { return c.name === _this.venta.moneda; }).price;
        if (this.venta.cantidadMax * price < this.venta.precio) {
            this.venta.precio = this.round10(this.venta.cantidadMax * price, -3);
        }
        this.venta.cantidad = this.round10(this.venta.precio / parseFloat(price.toString()), -5);
    };
    TransactionComponent.prototype.VentaOnKeyUpCantidad = function () {
        var _this = this;
        var price = this.coins.find(function (c) { return c.name === _this.venta.moneda; }).price;
        if (this.venta.cantidadMax < this.venta.cantidad) {
            this.venta.cantidad = this.round10(this.venta.cantidadMax, -5);
            this.venta.precio = this.round10(this.venta.cantidadMax * price, -3);
        }
        else {
            this.venta.precio = this.round10(this.venta.cantidad * price, -3);
            this.venta.cantidad = this.round10(this.venta.cantidad, -5);
        }
    };
    TransactionComponent.prototype.Comprar = function () {
        var _this = this;
        if (!this.compra.cantidad && !this.compra.precio && !this.compra.moneda) {
            this.mostrarModal('error', 'Compra', 'Complete los campos faltantes de compra.');
            return;
        }
        // activo el boton de cargando
        this.compra.procesando = true;
        var payload = {
            data: { type: 'compra', amount: this.compra.cantidad },
            moneda: this.coins.find(function (c) { return c.name === _this.compra.moneda; }).id
        };
        this.transac.CrearTransaccion(payload, function (res) {
            _this.compra = {
                cantidad: null,
                precio: null,
                procesando: false,
                habilitado: false,
                moneda: null
            };
            _this.TraerDatosMonedas();
            _this.mostrarModal('success', 'Compra', 'La compra se realizo exitosamente.');
        }, function (err) {
            _this.compra.procesando = false;
            _this.mostrarModal('error', 'Compra', 'Ocurrio un error al momento de realizar la compra.');
        });
    };
    TransactionComponent.prototype.Vender = function () {
        var _this = this;
        if (!this.venta.cantidad && !this.venta.moneda && !this.venta.precio) {
            this.mostrarModal('error', 'Venta', 'Complete los campos para realizar una venta.');
            return;
        }
        // activo el boton de cargando
        this.venta.procesando = true;
        var payload = {
            data: { type: 'venta', amount: this.venta.cantidad },
            moneda: this.coins.find(function (c) { return c.name === _this.venta.moneda; }).id
        };
        this.transac.CrearTransaccion(payload, function (res) {
            _this.venta = {
                cantidad: null,
                cantidadMax: null,
                precio: null,
                procesando: false,
                habilitado: false,
                moneda: null
            };
            _this.TraerDatosMonedas();
            _this.mostrarModal('success', 'Venta', 'La venta se realizo exitosamente.');
        }, function (err) {
            _this.venta.procesando = false;
            _this.mostrarModal('error', 'Venta', 'Ocurrio un error al momento de realizar la venta.');
        });
    };
    TransactionComponent.prototype.ActualizarBilletera = function () {
        var _this = this;
        this.transac.GetMyCoins(function (res) {
            _this.MisMonedas = res.body.filter(function (m) { return m.Balance > 0; }).map(function (w) {
                var name = _this.coins.find(function (c) { return c.id === w.NombreMoneda; }).name;
                return {
                    code: w.NombreMoneda,
                    name: name,
                    amount: w.Balance
                };
            });
            // this.carga.itemsVenta = false;
            setTimeout(function () {
                $('#selec-coin-sale').val('default');
                $('#selec-coin-sale').selectpicker('refresh');
            }, 200);
        }, function (err) {
            // this.carga.itemsVenta = false;
            _this.mostrarModal('error', 'Carga de Datos', 'Ocurrio un error al traer las monedas del cliente');
        });
    };
    TransactionComponent.prototype.TraerDatosMonedas = function () {
        var _this = this;
        this.carga.itemsTrans = false;
        this.http
            .get(config_1.apiTacs + '/cotizaciones')
            .subscribe(function (resp) {
            _this.coins = resp.map(function (coin) {
                return {
                    id: coin.id,
                    name: coin.name,
                    symbol: coin.symbol,
                    rank: coin.rank,
                    price: coin.price_usd,
                    last_update: coin.last_updated
                };
            });
            // this.carga.itemsCompra = false;
            setTimeout(function () {
                $('#selec-coin-purchase').val('default');
                $('#selec-coin-purchase').selectpicker('refresh');
            }, 200);
            _this.TraeMisTransacciones();
            _this.ActualizarBilletera();
        });
    };
    TransactionComponent.prototype.TraeMisTransacciones = function () {
        var _this = this;
        this.transac.GetMyTransactions(function (data) {
            data.sort(function (a, b) { return b.TransactionId - a.TransactionId; });
            _this.MisTransacciones = data.map(function (t) {
                return Object.assign({}, t, {
                    Coin: _this.coins.find(function (c) { return c.id === t.Coin; }).name
                });
            });
            _this.carga.itemsTrans = true;
        });
    };
    TransactionComponent.prototype.decimalAdjust = function (type, value, exp) {
        // Si el exp no está definido o es cero...
        if (typeof exp === 'undefined' || +exp === 0) {
            return Math[type](value);
        }
        value = +value;
        exp = +exp;
        // Si el valor no es un número o el exp no es un entero...
        if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0)) {
            return NaN;
        }
        // Shift
        value = value.toString().split('e');
        value = Math[type](+(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp)));
        // Shift back
        value = value.toString().split('e');
        return +(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp));
    };
    TransactionComponent.prototype.round10 = function (value, exp) {
        if (!value) {
            return value;
        }
        else {
            return this.decimalAdjust('round', value, exp);
        }
    };
    TransactionComponent.prototype.mostrarModal = function (type, title, message) {
        this.modal = {
            tipo: type,
            titulo: title,
            mensaje: message
        };
        $('#modal-popup').modal('show');
        setTimeout(function () { $('#modal-popup').modal('hide'); }, 4000);
    };
    TransactionComponent = __decorate([
        core_1.Component({
            selector: 'app-transaction',
            templateUrl: './transaction.component.html',
            styleUrls: ['./transaction.component.css']
        }),
        __metadata("design:paramtypes", [http_1.HttpClient, transaction_service_1.TransactionService])
    ], TransactionComponent);
    return TransactionComponent;
}());
exports.TransactionComponent = TransactionComponent;
//# sourceMappingURL=transaction.component.js.map