import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoListComponent } from './to-do-list/to-do-list.component';
import { ToDoListItemComponent } from './to-do-list-item/to-do-list-item.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { ShareComponent } from "./share/share.component"
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser'
import { ClipboardModule } from 'ngx-clipboard';



@NgModule({
  declarations: [
    ToDoListComponent,
    ToDoListItemComponent,
    ShareComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DragDropModule,
    BrowserModule,
    ClipboardModule,

  ]
})
export class ToDoListModule { }
