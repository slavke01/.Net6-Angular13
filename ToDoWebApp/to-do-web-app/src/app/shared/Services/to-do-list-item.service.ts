import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToDoListItem } from '../models/ToDoListItem.model';

@Injectable({
  providedIn: 'root',
})
export class ToDoListItemService {
  baseURL: string = 'https://localhost:7106/';
  constructor(private http: HttpClient) {}
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  getToDoListItem(id: number) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('id', id);
    return this.http.get<any>(this.baseURL + 'api/ToDoListItemControler', {
      params: queryParams,
    });
  }
  updatePosition(id: number, newPosition: number) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('id', id);
    queryParams = queryParams.append('position', newPosition);

    return this.http.patch<any>(
      this.baseURL + 'api/ToDoListItemControler',
      {},
      {
        params: queryParams,
      }
    );
  }
  editToDoListItem(data: ToDoListItem) {
    const body = JSON.stringify(data);
    return this.http.put<any>(
      this.baseURL + 'api/ToDoListItemControler',
      data,
      this.httpOptions
    );
  }
}
