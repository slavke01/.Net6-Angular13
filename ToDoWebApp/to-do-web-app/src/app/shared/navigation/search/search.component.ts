import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { TodolistService } from '../../Services/todolist.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
})
export class SearchComponent implements OnInit {
  searchForm: FormGroup;
  constructor(public router: Router, private service: TodolistService) {
    console.log(router.url);
  }

  ngOnInit(): void {
    this.service
      .getToDoLists("")
      .subscribe((res) => this.service.updateToDoListObservable(res));
  
    this.searchForm = new FormGroup({
      title: new FormControl(''),
    });
  }

  public onSubmit() {
    this.service
      .getToDoLists(this.searchForm.value['title'])
      .subscribe((res) => this.service.updateToDoListObservable(res));
  }
}
