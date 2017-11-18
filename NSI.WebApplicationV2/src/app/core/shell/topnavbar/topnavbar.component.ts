import { Component, OnInit } from '@angular/core';

declare let $: any;
@Component({
  selector: 'app-topnavbar',
  templateUrl: './topnavbar.component.html',
  styleUrls: ['./topnavbar.component.scss']
})
export class TopnavbarComponent implements OnInit {

  // TypeScript public modifier
  constructor() {

  }

  toggleClicked(event: MouseEvent) {
    const target = event.srcElement.id;
    const body = $('body');
    const menu = $('#sidebar-menu');

    // toggle small or large menu
    if (body.hasClass('nav-md')) {
      menu.find('li.active ul').hide();
      menu.find('li.active').addClass('active-sm').removeClass('active');
    } else {
      menu.find('li.active-sm ul').show();
      menu.find('li.active-sm').addClass('active').removeClass('active-sm');
    }
    body.toggleClass('nav-md nav-sm');

  }


  ngOnInit() {
    console.log('hello `topnavbar` component');
  }


}
