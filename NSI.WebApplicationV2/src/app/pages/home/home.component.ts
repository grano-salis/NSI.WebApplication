import { Component, OnInit } from '@angular/core';
import { QuoteService } from '../../services/quote.service';
import { CookieService } from 'ngx-cookie-service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  imageUrl: string;
  isLoading: boolean;
  token: string;
  constructor(private quoteService: QuoteService,
    private cookieService: CookieService) {
      //cookieService naravno treba obrisati - ostavljeno samo radi testiranja
  }

  ngOnInit() {
    this.token = this.cookieService.get('JWT.Token');
    this.isLoading = true;
    this.quoteService.getRandomQuote()
      .subscribe((r: any) => {
        this.isLoading = false;
        this.imageUrl = r.image;
      }, e => {
        this.isLoading = false;
        console.log('Error, ', e);
      });
  }

}
