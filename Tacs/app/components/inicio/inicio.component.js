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
var animations_1 = require("@angular/animations");
var inicio_service_1 = require("../../services/inicio.service");
var InicioComponent = /** @class */ (function () {
    function InicioComponent(servicio) {
        this.servicio = servicio;
        document.body.style.background = "linear-gradient(to left, #76b852, #8DC26F)";
    }
    InicioComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.servicio.InfoInicio.subscribe(function (result) {
            if (result.isError && result.msg) {
                _this.validation.error = { show: true, msg: result.msg };
            }
            else if (!result.isError && result.msg) {
                _this.validation.success = { show: true, msg: result.msg };
            }
            else {
                _this.validation = {
                    error: { show: false, msg: null },
                    success: { show: false, msg: null }
                };
            }
            // si no da clic sobre el aviso, desaparecera en 3s.
            setTimeout(function () { _this.toggle(); }, 3000);
        });
    };
    InicioComponent.prototype.toggle = function () {
        this.validation.error.show = false;
        this.validation.success.show = false;
    };
    InicioComponent.prototype.ngOnDestroy = function () {
        document.body.style.background = "white";
    };
    InicioComponent = __decorate([
        core_1.Component({
            selector: 'app-inicio',
            templateUrl: './inicio.component.html',
            styleUrls: ['./inicio.component.css'],
            animations: [
                animations_1.trigger('fadeMessage', [
                    animations_1.state('shown', animations_1.style({ opacity: 1, display: 'block' })),
                    animations_1.state('hidden', animations_1.style({ opacity: 0, display: 'none' })),
                    animations_1.transition('shown => hidden', animations_1.animate('1000ms ease-out')),
                    animations_1.transition('hidden => shown', animations_1.animate('300ms ease-in')),
                ])
            ]
        }),
        __metadata("design:paramtypes", [inicio_service_1.InicioService])
    ], InicioComponent);
    return InicioComponent;
}());
exports.InicioComponent = InicioComponent;
//# sourceMappingURL=inicio.component.js.map