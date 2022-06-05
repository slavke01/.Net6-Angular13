import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ToDoList } from '../models/ToDoList.model';
import { ToDoListItem } from '../models/ToDoListItem.model';
@Injectable({
  providedIn: 'root',
})
export class TodolistService {
  baseURL: string = 'https://localhost:7106/';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  private todo: ToDoList[] = [];
  private toDoListSubject = new BehaviorSubject(this.todo);

  
  public currentToDoLists = this.toDoListSubject.asObservable();


  constructor(private http: HttpClient) {}

  updateToDoListObservable(todo: ToDoList[]) {
    this.toDoListSubject.next(todo);
  }
  getToDoLists(title: string) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('title', title);
    return this.http.get<any>(this.baseURL + 'api/ToDoList', {
      params: queryParams,
    });
  }
  getToDoList(id: number) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('id', id);
    return this.http.get<any>(this.baseURL + 'GetById', {
      params: queryParams,
    });
  }
  editToDoList(data: ToDoList) {
    const body = JSON.stringify(data);
    return this.http.put<any>(
      this.baseURL + 'api/ToDoList',
      body,
      this.httpOptions
    );
  }
  deleteToDoList(id: number) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('id', id);
    return this.http.delete<any>(this.baseURL + 'api/ToDoList', {
      params: queryParams,
    });
  }
  addToDoList(data: ToDoList) {
    const body = JSON.stringify(data);
    return this.http.post<any>(
      this.baseURL + 'api/ToDoList',
      body,
      this.httpOptions
    );
  }
  shareList(data: ToDoList) {
    const body = JSON.stringify(data);
    return this.http.post<any>(
      this.baseURL + 'share',
      body,
      this.httpOptions
    );
  }
  getShared(id: number) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('shareId', id);
    return this.http.get<any>(this.baseURL + 'GetShared', {
      params: queryParams,
    });
  }

  updateReminder(reminder: boolean, id: number) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('id', id);
    queryParams = queryParams.append('reminder', reminder);
    return this.http.put<any>(
      this.baseURL + 'UpdateReminder',
      {},
      {
        params: queryParams,
      }
    );
  }
  updatePosition(id: number, newPosition: number) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('id', id);
    queryParams = queryParams.append('position', newPosition);

    return this.http.patch<any>(
      this.baseURL + 'api/ToDoList',
      {},
      {
        params: queryParams,
      }
    );
  }
  addItemToList(id: number, data: ToDoListItem) {
    const body = JSON.stringify(data);
    let queryParams = new HttpParams();
    queryParams = queryParams.append('id', id);
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      params: queryParams,
    };
    return this.http.post<any>(
      this.baseURL + 'AddListItemToList',
      body,
      httpOptions
    );
  }
}
