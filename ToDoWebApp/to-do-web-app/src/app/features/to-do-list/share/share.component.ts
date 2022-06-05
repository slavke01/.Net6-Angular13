import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToDoList } from 'src/app/shared/models/ToDoList.model';
import { TodolistService } from 'src/app/shared/Services/todolist.service';

@Component({
  selector: 'app-share',
  templateUrl: './share.component.html',
  styleUrls: ['./share.component.css'],
})
export class ShareComponent implements OnInit {
  constructor(private service: TodolistService, private router: Router, private route: ActivatedRoute,) {}

  shareid:number=0;
  shared:ToDoList;
  ngOnInit(): void {


    this.route.paramMap.subscribe((params) => {
      this.shareid = Number(params.get('shareid'));
      this.service.getShared(this.shareid).subscribe((res:ToDoList)=>{
        console.log(res);
        this.shared=res;
      });
    
    });

  }
}
