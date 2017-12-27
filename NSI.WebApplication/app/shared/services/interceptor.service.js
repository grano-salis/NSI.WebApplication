"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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
var http_1 = require("@angular/http");
require("rxjs/add/operator/do");
require("rxjs/add/operator/finally");
var Interceptor = /** @class */ (function (_super) {
    __extends(Interceptor, _super);
    function Interceptor(backend, defaultOptions) {
        var _this = _super.call(this, backend, defaultOptions) || this;
        _this.branchID = 0;
        _this.logonUrl = "\\";
        _this.jwtToken = jQuery("#app\\.token").val();
        return _this;
    }
    /**
    * Performs a request with `get` http method.
    * @param url
    * @param options
    * @returns {Observable<>}
    */
    Interceptor.prototype.get = function (url, options) {
        var _this = this;
        return _super.prototype.get.call(this, url, this.requestOptions(options))
            .do(function (res) {
        }, function (error) {
            _this.onError(error);
        })
            .finally(function () {
        });
    };
    /**
    * Performs a request with `post` http method.
    * @param url
    * @param body
    * @param options
    * @returns {Observable<>}
    */
    Interceptor.prototype.post = function (url, body, options) {
        var _this = this;
        return _super.prototype.post.call(this, url, body, this.requestOptions(options))
            .do(function (res) {
        }, function (error) {
            _this.onError(error);
        })
            .finally(function () {
        });
    };
    /**
    * Performs a request with `delete` http method.
    * @param url
    * @param options
    * @returns {Observable<>}
    */
    Interceptor.prototype.delete = function (url, options) {
        var _this = this;
        return _super.prototype.delete.call(this, url, this.requestOptions(options))
            .do(function (res) {
        }, function (error) {
            _this.onError(error);
        })
            .finally(function () {
        });
    };
    Interceptor.prototype.setBranchID = function (branchID) {
        this.branchID = branchID;
    };
    /**
     * Request options.
     * @param options
     * @returns {RequestOptionsArgs}
     */
    Interceptor.prototype.requestOptions = function (options) {
        if (options == null) {
            options = new http_1.RequestOptions();
        }
        if (options.headers == null) {
            options.headers = new http_1.Headers({
                'Content-Type': 'application/json',
                'JwtToken': this.jwtToken,
                'SelectedBranch': this.branchID
            });
        }
        return options;
    };
    /**
     * onError
     * @param error
     */
    Interceptor.prototype.onError = function (error) {
        if (!!error && error.status == 401) {
            alert("Error");
        }
    };
    Interceptor = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.ConnectionBackend, http_1.RequestOptions])
    ], Interceptor);
    return Interceptor;
}(http_1.Http));
exports.Interceptor = Interceptor;
//# sourceMappingURL=interceptor.service.js.map