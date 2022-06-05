import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ClipboardService } from 'ngx-clipboard';
import { ToDoList } from 'src/app/shared/models/ToDoList.model';
import { TodolistService } from 'src/app/shared/Services/todolist.service';

@Component({
  selector: 'app-to-do-preview',
  templateUrl: './to-do-preview.component.html',
  styleUrls: ['./to-do-preview.component.css'],
})
export class ToDoPreviewComponent implements OnInit {
  @Input() listItem: ToDoList;
  constructor(
    private router: Router,
    private service: TodolistService,
    private clipboardApi: ClipboardService
  ) {}

  ngOnInit(): void {}
  public edit(id: number) {
    this.service.updateReminder(false, id).subscribe();
    this.router.navigate(['to-do-list', id]);
  }
  public delete(id: number) {
    this.service.deleteToDoList(id).subscribe();
    location.reload();
  }
  public share(todo: ToDoList) {
    this.service.shareList(todo).subscribe((res) => {
      console.log('http://localhost:4200/to-do-list/' + res + '/share');

      this.clipboardApi.copyFromContent('http://localhost:4200/to-do-list/' + res + '/share');
    });
  }
}
