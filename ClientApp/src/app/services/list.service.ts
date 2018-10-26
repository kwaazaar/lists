import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';

import { ListSummary } from '../models/list-summary';
import { LogService } from './log.service';
import { ListModel } from '../models/list-model';
import { ListItem } from '../models/list-item';

@Injectable({
  providedIn: 'root'
})
export class ListService {

  private listApiUrl = 'api/list';

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private log: LogService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getListSummaries (): Observable<ListSummary[]> {
    return this.http.get<ListSummary[]>(this.baseUrl + this.listApiUrl)
      .pipe(
        tap(items => this.log.info(`fetched ${items.length} result(s)`)),
        catchError(this.handleError('getListSummaries', []))
      );
  }

  addList(list: ListModel): Observable<ListModel> {
    return this.http.post(this.baseUrl + this.listApiUrl, list, this.httpOptions)
      .pipe(
        tap((item: ListModel) => this.log.info(`list ${item.name} created`)),
        catchError(this.handleError<ListModel>('addList'))
      );
  }

  getList(id: number): Observable<ListModel> {
    return this.http.get(`${this.baseUrl}${this.listApiUrl}/${id}`)
      .pipe(
        tap((item: ListModel) => this.log.info(`list ${item.name} retrieved`)),
        catchError(this.handleError<ListModel>(`getList(id=${id})`))
      );
  }

  upsertListItem(listItem: ListItem): Observable<ListItem> {
    return this.http.post(`${this.baseUrl}${this.listApiUrl}/${listItem.listId}`, listItem, this.httpOptions)
      .pipe(
        tap((item: ListItem) => this.log.info(`listitem ${item.question} created/updated`)),
        catchError(this.handleError<ListItem>('upsertListItem'))
      );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: better job of transforming error for user consumption
      this.log.error(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of<T>(result);
    };
  }
}
