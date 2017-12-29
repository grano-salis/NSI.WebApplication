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
var platform_browser_1 = require("@angular/platform-browser");
var core_2 = require("@ngx-translate/core");
var merge_1 = require("rxjs/observable/merge");
var operators_1 = require("rxjs/operators");
var environment_1 = require("../environments/environment");
var logger_service_1 = require("./core/services/logger.service");
var i18n_service_1 = require("./core/services/i18n.service");
var log = new logger_service_1.Logger('App');
var AppComponent = (function () {
    function AppComponent(router, activatedRoute, titleService, translateService, i18nService) {
        this.router = router;
        this.activatedRoute = activatedRoute;
        this.titleService = titleService;
        this.translateService = translateService;
        this.i18nService = i18nService;
    }
    AppComponent.prototype.ngOnInit = function () {
        var _this = this;
        // Setup logger
        if (environment_1.environment.production) {
            logger_service_1.Logger.enableProductionMode();
        }
        log.debug('init');
        // Setup translations
        this.i18nService.init(environment_1.environment.defaultLanguage, environment_1.environment.supportedLanguages);
        var onNavigationEnd = this.router.events.pipe(operators_1.filter(function (event) { return event instanceof router_1.NavigationEnd; }));
        // Change page title on navigation or language change, based on route data
        merge_1.merge(this.translateService.onLangChange, onNavigationEnd)
            .pipe(operators_1.map(function () {
            var route = _this.activatedRoute;
            while (route.firstChild) {
                route = route.firstChild;
            }
            return route;
        }), operators_1.filter(function (route) { return route.outlet === 'primary'; }), operators_1.mergeMap(function (route) { return route.data; }))
            .subscribe(function (event) {
            var title = event['title'];
            if (title) {
                _this.titleService.setTitle(_this.translateService.instant(title));
            }
        });
    };
    AppComponent = __decorate([
        core_1.Component({
            selector: 'app-root',
            templateUrl: './app.component.html',
            styleUrls: ['./app.component.scss']
        }),
        __metadata("design:paramtypes", [router_1.Router,
            router_1.ActivatedRoute,
            platform_browser_1.Title,
            core_2.TranslateService,
            i18n_service_1.I18nService])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map