import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { ToDoList } from 'src/app/shared/models/ToDoList.model';
import { TodolistService } from 'src/app/shared/Services/todolist.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  response: ToDoList[] = [];
  constructor(private service: TodolistService) {}

  ngOnInit(): void {

    this.service.currentToDoLists.subscribe((res) => {
      this.response = res;
    });
   
  }

  drop(event: CdkDragDrop<ToDoList[]>) {
    this.service
      .updatePosition(
        this.response[event.previousIndex].id,
        event.currentIndex
      )
      .subscribe();
    moveItemInArray(this.response, event.previousIndex, event.currentIndex);
  }
}
