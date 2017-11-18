import { Component, OnInit } from '@angular/core';
import { AfterViewInit } from '@angular/core/src/metadata/lifecycle_hooks';

declare let $: any;
@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit, AfterViewInit {

  // TypeScript public modifiers
  private $BODY: any;
  private $MENU_TOGGLE: any;
  private $SIDEBAR_MENU: any;
  private $SIDEBAR_FOOTER: any;
  private $LEFT_COL: any;
  private $RIGHT_COL: any;
  private $NAV_MENU: any;
  private $FOOTER: any;

  constructor() {

  }

  ngAfterViewInit(): void {
    // this.plot();
  }

  anchorClicked(event: MouseEvent) {

    const target = event.srcElement.id;

    const $li = $('#' + target.replace("chevron", "li")).parent();

    if ($li.is('.active')) {
      $li.removeClass('active active-sm');
      $('ul:first', $li).slideUp(function () {
        //this.setContentHeight();
      });
    } else {
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
  }

  plot() {
    console.log('in sidebar');

    this.$BODY = $('body');
    this.$MENU_TOGGLE = $('#menu_toggle');
    this.$SIDEBAR_MENU = $('#sidebar-menu');
    this.$SIDEBAR_FOOTER = $('.sidebar-footer');
    this.$LEFT_COL = $('.left_col');
    this.$RIGHT_COL = $('.right_col');
    this.$NAV_MENU = $('.nav_menu');
    this.$FOOTER = $('footer');

    const $a = this.$SIDEBAR_MENU.find('a');
    this.$SIDEBAR_MENU.find('a').on('click', function (ev: any) {
      const $li = $(this).parent();

      if ($li.is('.active')) {
        $li.removeClass('active active-sm');
        $('ul:first', $li).slideUp(function () {
          this.setContentHeight();
        });
      } else {
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
      } else {
        this.$SIDEBAR_MENU.find('li.active-sm ul').show();
        this.$SIDEBAR_MENU.find('li.active-sm').addClass('active').removeClass('active-sm');
      }

      this.$BODY.toggleClass('nav-md nav-sm');

      this.setContentHeight();
    });

  }

  ngOnInit() {
    console.log('hello `sidebar` component');
  }

  setContentHeight() {
    // reset height
    this.$RIGHT_COL.css('min-height', $(window).height());

    const bodyHeight = this.$BODY.outerHeight(),
      footerHeight = this.$BODY.hasClass('footer_fixed') ? -10 : this.$FOOTER.height(),
      leftColHeight = this.$LEFT_COL.eq(1).height() + this.$SIDEBAR_FOOTER.height();
    let contentHeight = bodyHeight < leftColHeight ? leftColHeight : bodyHeight;

    // normalize content
    contentHeight -= this.$NAV_MENU.height() + footerHeight;

    this.$RIGHT_COL.css('min-height', contentHeight);
  }

}
