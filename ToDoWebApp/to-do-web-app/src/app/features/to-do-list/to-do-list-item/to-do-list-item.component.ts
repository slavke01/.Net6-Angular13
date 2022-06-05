import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToDoListItem } from 'src/app/shared/models/ToDoListItem.model';
import { ToDoListItemService } from 'src/app/shared/Services/to-do-list-item.service';

@Component({
  selector: 'app-to-do-list-item',
  templateUrl: './to-do-list-item.component.html',
  styleUrls: ['./to-do-list-item.component.css'],
})
export class ToDoListItemComponent implements OnInit {
  id: number;
  adding: boolean = false;
  todoListItem: ToDoListItem=new ToDoListItem();
  toDoListItemForm: FormGroup;
  @Output() itemEmitter = new EventEmitter<ToDoListItem>();
  constructor(
    private route: ActivatedRoute,
    private service: ToDoListItemService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.initForm();
    this.initData();
  }

  initFormData(data: ToDoListItem) {
    this.toDoListItemForm.get('name')?.setValue(data.name);
    this.toDoListItemForm.get('description')?.setValue(data.description);
  }
  initForm() {
    this.toDoListItemForm = new FormGroup({
      name: new FormControl(null, Validators.required),
      description: new FormControl(null, Validators.required),
    });
  }
  initData() {
    this.route.paramMap.subscribe((params) => {
      this.id = Number(params.get('itemid'));
      if (this.id == 0) {
        this.adding = true;
      }

      if (this.adding == false) {
        this.service.getToDoListItem(this.id).subscribe((todolistitem) => {
          this.todoListItem = todolistitem;
          this.initFormData(todolistitem);
        });
      }
    });
  }
  public onSubmit() {
    this.todoListItem.name = this.toDoListItemForm.value['name'];
      this.todoListItem.description =
        this.toDoListItemForm.value['description'];
    if (this.adding == false) {
      this.service
        .editToDoListItem(this.todoListItem)
        .subscribe((res) =>
          this.router.navigate(['to-do-list', this.todoListItem.toDoListId])
        );
    }else{
      this.itemEmitter.emit(this.todoListItem);
    }



  }
}
