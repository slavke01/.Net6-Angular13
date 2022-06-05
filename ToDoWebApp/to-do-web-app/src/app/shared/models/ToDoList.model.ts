import { Deserializale } from "./deserializable.model";
import { ToDoListItem } from "./ToDoListItem.model";


export class ToDoList implements Deserializale{

id:number=0;
title:string="";
position:number=0;
remind:boolean=true;
remindafter:Date;
remindEmail:string="";
opened:boolean=true;
owner:string="";
listItems:ToDoListItem[]=[];

deserialize(input: any) {
   Object.assign(this , input);
   return this;
}
}