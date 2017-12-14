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
var hearing_1 = require("./hearing");
var hearings_service_1 = require("../../../services/hearings.service");
var users_service_1 = require("../../../services/users.service");
var router_1 = require("@angular/router");
var HearingNewComponent = (function () {
    function HearingNewComponent(hearingsService, usersService, route) {
        this.hearingsService = hearingsService;
        this.usersService = usersService;
        this.route = route;
        this.edit = false;
        this.query = '';
        this.filteredList = [];
        this.notes = [];
        this.model = new hearing_1.Hearing();
    }
    HearingNewComponent.prototype.ngAfterViewInit = function () {
        $('#hearingDate').datetimepicker();
    };
    HearingNewComponent.prototype.filter = function () {
        var _this = this;
        if (this.query.length > 2) {
            this.usersService.getForHearings(this.query).subscribe(function (data) {
                console.log(data);
                _this.filteredList = data;
            });
        }
        else {
            this.filteredList = [];
        }
    };
    HearingNewComponent.prototype.add = function (item) {
        this.model.userHearing.push(item);
        this.query = '';
        this.filteredList = [];
    };
    HearingNewComponent.prototype.remove = function (item) {
        this.model.userHearing.splice(this.model.userHearing.indexOf(item), 1);
    };
    //edit-update
    HearingNewComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.id = +this.route.snapshot.paramMap.get('id');
        this.hearingsService.getHearingById(this.id).subscribe(function (data) {
            if (data != null) {
                console.log(data);
                _this.edit = true;
                _this.model.hearingDate = new Date(data.hearingDate).toLocaleString();
                _this.model.userHearing = data.userHearing;
                _this.model.note = data.note;
            }
            console.log(_this.edit);
        });
    };
    HearingNewComponent.prototype.updateHearing = function () {
        this.model.hearingDate = new Date($('#hearingDate').val()).toLocaleString();
        this.hearingsService.putHearing(this.id, this.model).subscribe(function (r) { return console.log('Saljemo update: ' + r); }, function (error) { return console.log("Error: " + error.message); });
    };
    HearingNewComponent.prototype.onSubmit = function () {
        console.log("Form submitted");
        this.model.hearingDate = new Date($('#hearingDate').val()).toLocaleString();
        this.notes.push({ text: this.noteText, createdByUserId: 1, hearingId: 5 });
        this.model.note = this.notes;
        this.model.createdByUserId = 1;
        this.model.caseId = 3;
        console.log(this.model);
        this.hearingsService.postHearing(this.model).subscribe(function (r) { return console.log('Hazime imamo bingo: ' + r); }, function (error) { return console.log("Error: " + error.message); });
    };
    HearingNewComponent.prototype.newHearing = function () {
        this.model = new hearing_1.Hearing();
    };
    HearingNewComponent.prototype.deleteHearing = function () {
        this.hearingsService.deleteHearingById(this.id).subscribe(function (r) { return console.log('Brisemo hearing:' + r); }, function (error) { return console.log("Error: " + error.message); });
    };
    HearingNewComponent = __decorate([
        core_1.Component({
            selector: 'app-hearing-new',
            templateUrl: './hearing-new.component.html',
            styleUrls: ['./hearing-new.component.scss']
        }),
        __metadata("design:paramtypes", [hearings_service_1.HearingsService, users_service_1.UsersService, router_1.ActivatedRoute])
    ], HearingNewComponent);
    return HearingNewComponent;
}());
exports.HearingNewComponent = HearingNewComponent;
//# sourceMappingURL=hearing-new.component.js.map