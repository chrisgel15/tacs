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
var RegistroComponent = /** @class */ (function () {
    function RegistroComponent(servicio, router) {
        this.servicio = servicio;
        this.router = router;
        this.username = '';
        this.password = '';
        this.procesando = false;
    }
    RegistroComponent.prototype.ngOnInit = function () { };
    RegistroComponent.prototype.valEnter = function (event) {
        if (event.keyCode === 13) {
            document.getElementById("signup").click();
        }
    };
    RegistroComponent.prototype.validarCampos = function () {
        var validation;
        if (this.username == '' || !/^([a-z0-9]{5,})$/.test(this.username.toLowerCase())) {
            validation = { isError: true, msg: 'Username incorrecto, minimo 5 caracteres alfanumerico.' };
        }
        else if (this.password == '' || !/^([a-z0-9]{8,})$/.test(this.password.toLowerCase())) {
            validation = { isError: true, msg: 'Password incorrecto, minimo 8 caracteres alfanumerico.' };
        }
        else {
            validation = { isError: false, msg: null };
        }
        this.servicio.EmitirError(validation);
        return validation.isError;
    };
    RegistroComponent.prototype.registrar = function () {
        var _this = this;
        if (!this.validarCampos()) {
            this.procesando = true;
            this.servicio.Registrar({ username: this.username, password: this.password }, function (response) {
                if (response.status >= 400) {
                    _this.servicio.EmitirError({ isError: true, msg: 'Ocurrio un error, intente de nuevo.' });
                }
                if (response.status >= 200 && response.status < 300) {
                    _this.servicio.EmitirError({ isError: false, msg: 'Se creo exitosamente el usuario.' });
                    _this.router.navigate(['/']);
                }
                _this.procesando = false;
            });
        }
    };
    RegistroComponent = __decorate([
        core_1.Component({
            selector: 'app-registro',
            templateUrl: './registro.component.html',
            styleUrls: ['./registro.component.css']
        }),
        __metadata("design:paramtypes", [inicio_service_1.InicioService, router_1.Router])
    ], RegistroComponent);
    return RegistroComponent;
}());
exports.RegistroComponent = RegistroComponent;
//# sourceMappingURL=registro.component.js.map