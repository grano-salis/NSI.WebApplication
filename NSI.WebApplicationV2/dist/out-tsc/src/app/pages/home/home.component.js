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
var quote_service_1 = require("../../services/quote.service");
var ngx_cookie_service_1 = require("ngx-cookie-service");
var tasks_service_1 = require("../../services/tasks.service");
var logger_service_1 = require("../../core/services/logger.service");
var logger = new logger_service_1.Logger('Home');
var HomeComponent = (function () {
    function HomeComponent(quoteService, cookieService, tasksService) {
        this.quoteService = quoteService;
        this.cookieService = cookieService;
        this.tasksService = tasksService;
        //cookieService naravno treba obrisati - ostavljeno samo radi testiranja
    }
    HomeComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.token = this.cookieService.get('JWT.Token');
        this.isLoading = true;
        this.quoteService.getRandomQuote()
            .subscribe(function (r) {
            _this.isLoading = false;
            _this.imageUrl = r.image;
        }, function (e) {
            _this.isLoading = false;
            console.log('Error, ', e);
        });
        this.loadTasks();
    };
    HomeComponent.prototype.loadTasks = function () {
        this.tasksService.getTasks()
            .subscribe(function (r) {
            logger.debug('Load tasks: ', r);
        });
    };
    HomeComponent = __decorate([
        core_1.Component({
            selector: 'app-home',
            templateUrl: './home.component.html',
            styleUrls: ['./home.component.scss']
        }),
        __metadata("design:paramtypes", [quote_service_1.QuoteService,
            ngx_cookie_service_1.CookieService, tasks_service_1.TasksService])
    ], HomeComponent);
    return HomeComponent;
}());
exports.HomeComponent = HomeComponent;
//# sourceMappingURL=home.component.js.map