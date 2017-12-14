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
var meeting_1 = require("./meeting");
var meetings_service_1 = require("../../../services/meetings.service");
var users_service_1 = require("../../../services/users.service");
var router_1 = require("@angular/router");
var MeetingNewComponent = (function () {
    function MeetingNewComponent(meetingsService, usersService, route) {
        this.meetingsService = meetingsService;
        this.usersService = usersService;
        this.route = route;
        this.edit = false;
        this.query = '';
        this.filteredList = [];
        this.model = new meeting_1.Meeting();
    }
    MeetingNewComponent.prototype.ngAfterViewInit = function () {
        $('#from').datetimepicker();
        $('#to').datetimepicker({
            useCurrent: false //Important! See issue #1075
        });
        $("#from").on("dp.change", function (e) {
            $('#to').data("DateTimePicker").minDate(e.date);
        });
        $("#to").on("dp.change", function (e) {
            $('#from').data("DateTimePicker").maxDate(e.date);
        });
    };
    MeetingNewComponent.prototype.filter = function () {
        var _this = this;
        if (this.query.length > 2) {
            this.usersService.getForMeetings(this.query).subscribe(function (data) {
                console.log(data);
                _this.filteredList = data;
            });
        }
        else {
            this.filteredList = [];
        }
    };
    MeetingNewComponent.prototype.add = function (item) {
        this.model.userMeeting.push(item);
        this.query = '';
        this.filteredList = [];
    };
    MeetingNewComponent.prototype.remove = function (item) {
        this.model.userMeeting.splice(this.model.userMeeting.indexOf(item), 1);
    };
    MeetingNewComponent.prototype.onSubmit = function () {
        this.model.from = new Date($('#from').val()).toLocaleString();
        this.model.to = new Date($('#to').val()).toLocaleString();
        console.log(this.model);
        this.meetingsService.postMeeting(this.model).subscribe(function (r) { return console.log('Hazime imamo bingo: ' + r); }, function (error) { return console.log("Error: " + error.message); });
    };
    MeetingNewComponent.prototype.newMeeting = function () {
        this.model = new meeting_1.Meeting();
    };
    //edit-update
    MeetingNewComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.id = +this.route.snapshot.paramMap.get('id');
        this.meetingsService.getMeetingById(this.id).subscribe(function (data) {
            if (data != null) {
                _this.edit = true;
                _this.model.title = data.title;
                _this.model.from = new Date(data.from).toLocaleString();
                _this.model.to = new Date(data.to).toLocaleString();
                _this.model.userMeeting = data.userMeeting;
            }
            console.log(_this.edit);
        });
    };
    MeetingNewComponent.prototype.updateMeeting = function () {
        this.model.from = new Date($('#from').val()).toLocaleString();
        this.model.to = new Date($('#to').val()).toLocaleString();
        this.meetingsService.putMeeting(this.id, this.model).subscribe(function (r) { return console.log('Saljemo update: ' + r); }, function (error) { return console.log("Error: " + error.message); });
    };
    MeetingNewComponent.prototype.deleteMeeting = function () {
        console.log(this.id);
        this.meetingsService.deleteMeetingById(this.id).subscribe(function (r) { return console.log('Brisemo meeting:' + r); }, function (error) { return console.log("Error: " + error.message); });
    };
    MeetingNewComponent = __decorate([
        core_1.Component({
            selector: 'app-meeting-new',
            templateUrl: './meeting-new.component.html',
            styleUrls: ['./meeting-new.component.scss']
        }),
        __metadata("design:paramtypes", [meetings_service_1.MeetingsService, users_service_1.UsersService, router_1.ActivatedRoute])
    ], MeetingNewComponent);
    return MeetingNewComponent;
}());
exports.MeetingNewComponent = MeetingNewComponent;
//# sourceMappingURL=meeting-new.component.js.map