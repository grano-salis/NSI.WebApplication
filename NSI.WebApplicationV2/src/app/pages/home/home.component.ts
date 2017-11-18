import { Component, OnInit } from '@angular/core';
import { QuoteService } from '../../services/quote.service';
import { CookieService } from 'ngx-cookie-service';
import { TasksService } from '../../services/tasks.service';
import { Logger } from '../../core/services/logger.service';

const logger = new Logger('Home');
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
    private cookieService: CookieService, private tasksService: TasksService) {
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

    this.loadTasks();
  }

  loadTasks() {
    this.tasksService.getTasks()
      .subscribe((r: any) => {
        logger.debug('Load tasks: ', r);
      });
  }
}
