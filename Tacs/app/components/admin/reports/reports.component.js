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
var report_service_1 = require("../../../services/report.service");
var ReportsComponent = /** @class */ (function () {
    function ReportsComponent(reportService) {
        this.reportService = reportService;
        document.body.style.background = "linear-gradient(to left, #76b852, #8DC26F)";
    }
    ReportsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.reportService.getReports().subscribe(function (x) { return _this.transacciones = x; });
    };
    ReportsComponent.prototype.ngOnDestroy = function () {
        document.body.style.background = "whiite";
    };
    ReportsComponent = __decorate([
        core_1.Component({
            selector: 'app-reports',
            templateUrl: './reports.component.html',
            styleUrls: ['./reports.component.css']
        }),
        __metadata("design:paramtypes", [report_service_1.ReportService])
    ], ReportsComponent);
    return ReportsComponent;
}());
exports.ReportsComponent = ReportsComponent;
//# sourceMappingURL=reports.component.js.map