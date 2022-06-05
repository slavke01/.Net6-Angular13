import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { ToDoList } from 'src/app/shared/models/ToDoList.model';
import { FormGroup, FormControl, FormArray, Validators } from '@angular/forms';
import { TodolistService } from 'src/app/shared/Services/todolist.service';
import { ToDoListItem } from 'src/app/shared/models/ToDoListItem.model';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { ToDoListItemService } from 'src/app/shared/Services/to-do-list-item.service';
@Component({
  selector: 'app-to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.css'],
})
export class ToDoListComponent implements OnInit {
  id: number;
  today = new Date();
  adding: boolean = false;
  todoList: ToDoList = new ToDoList();
  toDoListForm: FormGroup;
  reminderForm: FormGroup;
  constructor(
    private route: ActivatedRoute,
    private service: TodolistService,
    private router: Router,
    private itemService: ToDoListItemService
  ) {}
  ngOnInit(): void {
    this.initData();
    this.initForm();
  }

  initFormData(data: ToDoList) {
    this.toDoListForm.get('title')?.setValue(data.title);
  }
  initForm() {
    this.toDoListForm = new FormGroup({
      title: new FormControl(null, Validators.required),
    });
    this.reminderForm = new FormGroup({
      remindDate: new FormControl(null, Validators.required),
    });
  }
  initData() {
    this.route.paramMap.subscribe((params) => {
      this.id = Number(params.get('id'));
      if (this.id == 0) {
        this.adding = true;
      }
      if (this.adding == false) {
        this.service.getToDoList(this.id).subscribe((todolist) => {
          this.todoList = todolist;
          this.initFormData(todolist);
        });
      }
    });
  }

  public editItem(id: number) {
    this.router.navigate(['to-do-list-item', id]);
  }
  public onSubmit() {
    this.todoList.title = this.toDoListForm.value['title'];
    this.service.editToDoList(this.todoList).subscribe();
  }
  public onRemindSubmit() {
    if (this.reminderForm.valid && this.id != 0) {
      this.todoList.remind = true;
      this.todoList.remindafter = this.reminderForm.value['remindDate'];

      this.service.editToDoList(this.todoList).subscribe();
    }
  }
  public valid(): boolean {
    if (
      this.reminderForm.controls['remindDate'].invalid &&
      (this.reminderForm.controls['remindDate'].dirty ||
        this.reminderForm.controls['remindDate'].touched)
    ) {
      return true;
    }

    return false;
  }
  public getData(value: ToDoListItem): void {
    if (this.id == 0) {
      this.todoList.title = this.toDoListForm.value['title'];
      this.todoList.listItems.push(value);
      this.service.addToDoList(this.todoList).subscribe((res: number) => {
        this.router.navigate(['to-do-list', res]);
      });
    } else {
      this.service
        .addItemToList(this.id, value)
        .subscribe((res) => location.reload());
    }
  }
  drop(event: CdkDragDrop<ToDoListItem[]>) {
    this.itemService
      .updatePosition(
        this.todoList.listItems[event.previousIndex].id,
        event.currentIndex
      )
      .subscribe();

    moveItemInArray(
      this.todoList.listItems,
      event.previousIndex,
      event.currentIndex
    );
    }
}
