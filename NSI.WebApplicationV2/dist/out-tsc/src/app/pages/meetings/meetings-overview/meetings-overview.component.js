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
var lodash_1 = require("lodash");
var moment = require("moment");
var logger_service_1 = require("../../../core/services/logger.service");
var helper_service_1 = require("../../../services/helper.service");
var meetings_service_1 = require("../../../services/meetings.service");
var logger = new logger_service_1.Logger('Meetings');
var MeetingsComponent = (function () {
    function MeetingsComponent(meetingsService) {
        var _this = this;
        this.meetingsService = meetingsService;
        this.color = '#223442';
        this.calendarConfiguration = {
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            defaultDate: moment().format('YYYY-MM-DD'),
            selectable: true,
            selectHelper: true,
            editable: true,
            eventLimit: true,
            events: []
        };
        this.eventModel = {
            dateFrom: null,
            dateTo: null,
            title: '',
            desc: ''
        };
        this.calendarConfiguration.select = function (start, end) { return _this._onSelect(start, end); };
    }
    // ****************
    // Public methods
    // ****************
    MeetingsComponent.prototype.ngOnInit = function () {
        this.loadMeetings();
    };
    MeetingsComponent.prototype.submitEvent = function (form) {
        this.formSubmitted = true;
        $(this._calendar).fullCalendar('unselect');
        if (form.invalid) {
            return;
        }
        var eventData = {
            start: this.eventModel.dateFrom,
            end: this.eventModel.dateTo,
            title: this.eventModel.title
        };
        $(this._calendar).fullCalendar('renderEvent', eventData, true);
        this.dateSelected = false;
        this.formSubmitted = false;
        $(this._calendar).fullCalendar('unselect');
        //todo: add service
    };
    MeetingsComponent.prototype.cancelNewEvent = function () {
        this.eventModel = {
            dateFrom: null,
            dateTo: null,
            title: '',
            desc: ''
        };
        this.dateSelected = false;
    };
    MeetingsComponent.prototype.onCalendarReady = function (calendar) {
        this._calendar = calendar;
        $(this._calendar).fullCalendar('option', 'contentHeight', 650);
    };
    // ****************
    // Private methods
    // ****************
    MeetingsComponent.prototype._onSelect = function (start, end) {
        if (this._calendar != null) {
            this.dateSelected = true;
            helper_service_1.HelperService.scrollToTop();
            this.eventModel = {
                dateFrom: start,
                dateTo: end
            };
            // if (title) {
            //   eventData = {
            //     title: title,
            //     start: start,
            //     end: end
            //   };
            //   $(this._calendar).fullCalendar('renderEvent', eventData, true);
            // }
        }
    };
    MeetingsComponent.prototype.loadMeetings = function () {
        var _this = this;
        this.meetingsService.getMeetings()
            .subscribe(function (r) {
            var temp = [];
            lodash_1.each(r, function (item) {
                temp.push({
                    title: 'Meeting ID: ' + item.meetingId,
                    start: item.from,
                    end: item.to,
                    color: _this.color
                });
            });
            $(_this._calendar).fullCalendar('renderEvents', temp, true);
        });
    };
    MeetingsComponent.prototype.getData = function () {
        return [
            {
                title: 'All Day Event',
                start: '2017-11-01',
                color: '#223442'
            },
            {
                title: 'Long Event',
                start: '2017-11-07',
                end: '2017-12-10',
                color: '#223442'
            },
            {
                title: 'Dinner',
                start: '2017-11-14T20:00:00',
                color: '#223442'
            },
            {
                title: 'Birthday Party',
                start: '2017-11-01T07:00:00',
                color: '#223442'
            }
        ];
    };
    MeetingsComponent = __decorate([
        core_1.Component({
            selector: 'app-meetings',
            templateUrl: './meetings-overview.component.html',
            styleUrls: ['./meetings-overview.component.scss']
        }),
        __metadata("design:paramtypes", [meetings_service_1.MeetingsService])
    ], MeetingsComponent);
    return MeetingsComponent;
}());
exports.MeetingsComponent = MeetingsComponent;
//# sourceMappingURL=meetings-overview.component.js.map