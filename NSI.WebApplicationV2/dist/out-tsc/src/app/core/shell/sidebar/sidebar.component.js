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
var SidebarComponent = (function () {
    function SidebarComponent() {
    }
    SidebarComponent.prototype.ngAfterViewInit = function () {
        // this.plot();
    };
    SidebarComponent.prototype.anchorClicked = function (event) {
        // console.log("anchorClicked", event);
        var target = event.target.id;
        var $li = $('#' + target.replace("chevron", "li")).parent();
        if ($li.is('.active')) {
            $li.removeClass('active active-sm');
            $('ul:first', $li).slideUp(function () {
                //this.setContentHeight();
            });
        }
        else {
            // prevent closing menu if we are on child menu
            if (!$li.parent().is('.child_menu')) {
                $('#sidebar-menu').find('li').removeClass('active active-sm');
                $('#sidebar-menu').find('li ul').slideUp();
            }
            $li.addClass('active');
            $('ul:first', $li).slideDown(function () {
                //this.setContentHeight();
            });
        }
    };
    SidebarComponent.prototype.plot = function () {
        console.log('in sidebar');
        this.$BODY = $('body');
        this.$MENU_TOGGLE = $('#menu_toggle');
        this.$SIDEBAR_MENU = $('#sidebar-menu');
        this.$SIDEBAR_FOOTER = $('.sidebar-footer');
        this.$LEFT_COL = $('.left_col');
        this.$RIGHT_COL = $('.right_col');
        this.$NAV_MENU = $('.nav_menu');
        this.$FOOTER = $('footer');
        var $a = this.$SIDEBAR_MENU.find('a');
        this.$SIDEBAR_MENU.find('a').on('click', function (ev) {
            var $li = $(this).parent();
            if ($li.is('.active')) {
                $li.removeClass('active active-sm');
                $('ul:first', $li).slideUp(function () {
                    this.setContentHeight();
                });
            }
            else {
                // prevent closing menu if we are on child menu
                if (!$li.parent().is('.child_menu')) {
                    this.$SIDEBAR_MENU.find('li').removeClass('active active-sm');
                    this.$SIDEBAR_MENU.find('li ul').slideUp();
                }
                $li.addClass('active');
                $('ul:first', $li).slideDown(function () {
                    this.setContentHeight();
                });
            }
        });
        // toggle small or large menu
        this.$MENU_TOGGLE.on('click', function () {
            if (this.$BODY.hasClass('nav-md')) {
                this.$SIDEBAR_MENU.find('li.active ul').hide();
                this.$SIDEBAR_MENU.find('li.active').addClass('active-sm').removeClass('active');
            }
            else {
                this.$SIDEBAR_MENU.find('li.active-sm ul').show();
                this.$SIDEBAR_MENU.find('li.active-sm').addClass('active').removeClass('active-sm');
            }
            this.$BODY.toggleClass('nav-md nav-sm');
            this.setContentHeight();
        });
    };
    SidebarComponent.prototype.ngOnInit = function () {
        console.log('hello `sidebar` component');
    };
    SidebarComponent.prototype.setContentHeight = function () {
        // reset height
        this.$RIGHT_COL.css('min-height', $(window).height());
        var bodyHeight = this.$BODY.outerHeight(), footerHeight = this.$BODY.hasClass('footer_fixed') ? -10 : this.$FOOTER.height(), leftColHeight = this.$LEFT_COL.eq(1).height() + this.$SIDEBAR_FOOTER.height();
        var contentHeight = bodyHeight < leftColHeight ? leftColHeight : bodyHeight;
        // normalize content
        contentHeight -= this.$NAV_MENU.height() + footerHeight;
        this.$RIGHT_COL.css('min-height', contentHeight);
    };
    SidebarComponent = __decorate([
        core_1.Component({
            selector: 'app-sidebar',
            templateUrl: './sidebar.component.html',
            styleUrls: ['./sidebar.component.scss']
        }),
        __metadata("design:paramtypes", [])
    ], SidebarComponent);
    return SidebarComponent;
}());
exports.SidebarComponent = SidebarComponent;
//# sourceMappingURL=sidebar.component.js.map