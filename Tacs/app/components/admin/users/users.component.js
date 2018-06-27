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
var admin_service_1 = require("../../../services/admin.service");
var moment = require("moment");
var UsersComponent = /** @class */ (function () {
    function UsersComponent(service) {
        this.service = service;
        this.criteria = { nombre: '', fecha: '' };
        this.comparacion = [];
        this.comparando = false;
        this.compareResultado = {
            user1: { name: null, total: null },
            user2: { name: null, total: null },
            resultado: null
        };
        this.message = { isOk: false, msg: null };
        document.body.style.background = "linear-gradient(to left, #76b852, #8DC26F)";
        $(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    }
    UsersComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.service.getUsers(function (data) {
            _this.usuarios = data;
            _this.filtrados = data;
        });
    };
    UsersComponent.prototype.ngOnDestroy = function () {
        document.body.style.background = "whiite";
    };
    UsersComponent.prototype.buscarUsuariosPorCriterio = function () {
        this.filtrados = this.usersByCriteria();
    };
    UsersComponent.prototype.limpiarFiltros = function () {
        this.criteria.fecha = '';
        this.criteria.nombre = '';
        this.filtrados = this.usuarios;
        this.comparacion.map(function (i) {
            document.getElementById('usuario-' + i).style.background = "white";
        });
        this.comparacion = [];
    };
    UsersComponent.prototype.usersByCriteria = function () {
        var nombre = this.criteria.nombre;
        var fecha = moment(this.criteria.fecha, 'YYYY-MM-DD').format('MM/DD/YYYY');
        if (nombre === '' && fecha === '') {
            return this.usuarios;
        }
        else {
            return this.usuarios.filter(function (u) {
                if (nombre !== '' && fecha !== '') {
                    var access = moment(u.LastAccess, 'DD/MM/YYYY').format('MM/DD/YYYY');
                    return u.Name.includes(nombre) || Date.parse(access) === Date.parse(fecha);
                }
                else if (nombre !== '') {
                    return u.Name.includes(nombre);
                }
                else if (fecha !== '') {
                    var access = moment(u.LastAccess, 'DD/MM/YYYY').format('MM/DD/YYYY');
                    return Date.parse(access) === Date.parse(fecha);
                }
                else {
                    return false;
                }
            });
        }
    };
    UsersComponent.prototype.seleccionarUsuario = function (id) {
        if (this.comparacion.includes(id)) {
            var i = this.comparacion.shift();
            document.getElementById('usuario-' + i).style.backgroundColor = 'white';
        }
        else {
            var l = this.comparacion.length;
            if (l === 2) {
                var i = this.comparacion.shift();
                document.getElementById('usuario-' + i).style.backgroundColor = 'white';
                this.comparacion.push(id);
                document.getElementById('usuario-' + id).style.backgroundColor = '#ade28f';
            }
            else {
                this.comparacion.push(id);
                document.getElementById('usuario-' + id).style.backgroundColor = '#ade28f';
            }
        }
        console.log(this.comparacion);
    };
    UsersComponent.prototype.compararDosUsuarios = function () {
        var _this = this;
        this.comparando = true;
        this.message = { isOk: false, msg: null };
        if (this.comparacion.length < 2) {
            this.message = { isOk: false, msg: 'Elija 2 usuarios para comparar.' };
            $('#modalResponse').modal('show');
            this.comparando = false;
        }
        else {
            var u1_1 = this.usuarios.find(function (u) { return u.Id === _this.comparacion[0]; }).Name;
            var u2_1 = this.usuarios.find(function (u) { return u.Id === _this.comparacion[1]; }).Name;
            this.service.compareUsers(u1_1, u2_1, function (res) {
                var data = res.body.Result;
                var resultado;
                if (data.Patrimonio1 === data.Patrimonio2) {
                    resultado = 'iguales';
                }
                else if (data.Patrimonio1 > data.Patrimonio2) {
                    resultado = 'mayor';
                }
                else {
                    resultado = 'menor';
                }
                // this.message = { isOk: true, msg: resultado };
                _this.compareResultado = {
                    user1: { name: u1_1, total: data.Patrimonio1 },
                    user2: { name: u2_1, total: data.Patrimonio2 },
                    resultado: resultado
                };
                $('#compareResponse').modal('show');
                _this.comparando = false;
                _this.comparacion.map(function (i) {
                    document.getElementById('usuario-' + i).style.background = "white";
                });
                _this.comparacion = [];
            }, function (err) {
                if (err.status >= 400) {
                    console.log(err);
                    _this.message = {
                        isOk: false,
                        msg: 'Ocurrio un error al realizar la comparacion'
                    };
                    $('#modalResponse').modal('show');
                }
                _this.comparando = false;
            });
        }
    };
    UsersComponent = __decorate([
        core_1.Component({
            selector: 'app-users',
            templateUrl: './users.component.html',
            styleUrls: ['./users.component.css']
        }),
        __metadata("design:paramtypes", [admin_service_1.AdminService])
    ], UsersComponent);
    return UsersComponent;
}());
exports.UsersComponent = UsersComponent;
//# sourceMappingURL=users.component.js.map