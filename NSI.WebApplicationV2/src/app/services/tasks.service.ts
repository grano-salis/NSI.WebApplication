import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Tasks } from '../pages/tasks/new-task/tasks';


@Injectable()
export class TasksService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/api/tasks';
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  }

  postTasks(task: Tasks): Observable<any> {
    let body = JSON.stringify(task);
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post(this._url, body, { headers: headers });
  }

  putTask(id: number, task: Tasks): Observable<any> {
    let body = JSON.stringify(task);
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.put(this._url + "/" + id, body, { headers: headers });
  }

   getTasks(params?: any): Observable<any> {
     return this.http.get(`${this._url}`);
   }

  getTasksWithDueDateRange(params?: any) {
    return this.http.get(`${this._url}/getTasksWithDueDateRange`, {params: params})
  }
}
