import { Deserializale } from "./deserializable.model";

export class ToDoListItem implements Deserializale{
    id:number;
    name:string;
    description:string;
    position:number;
    toDoListId:number;


    deserialize(input: any) {
        Object.assign(this , input)
        return this;
     }
}