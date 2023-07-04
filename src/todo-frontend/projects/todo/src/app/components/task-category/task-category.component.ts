import { Component, EventEmitter, Input, inject, Output } from '@angular/core';
import { CommonModule, SlicePipe } from '@angular/common';
import { TodoTask } from '../../models/todo-task';
import { DataService } from '../../services/data.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'todo-task-category',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './task-category.component.html',
  styleUrls: ['./task-category.component.css']
})
export class TaskCategoryComponent {

  @Input() taskList : TodoTask[] = [];
  @Output() onUpdate = new EventEmitter<TodoTask>();
  @Output() onDelete = new EventEmitter<number>();
  @Output() onSelectedTaskChange = new EventEmitter<TodoTask>();
  editMode: boolean = false;
  selectedTaskId: number = -1;
  taskDescription: string| null = null;

  dataService: DataService = inject(DataService);

  taskTrackBy(index: number, task: TodoTask) {
    return task.id;
  }

  changeSelectedTask(taskId: number) {
    this.selectedTaskId = taskId;
    let selectedTask = this.taskList.find(task => task.id == taskId);
    this.onSelectedTaskChange.emit(selectedTask);
  }

  editTask(taskId: number, taskDescription: string) {
    this.editMode = true;
    this.taskDescription = taskDescription;
    console.log(this.editMode);
  }

  cancelEdit(taskId: number) {
    this.editMode = false;
  }

  updateTask(taskId: number) {
    let taskToBeUpdated = this.taskList.find(task => task.id == taskId);
    if(taskToBeUpdated && this.taskDescription && this.taskDescription.trim() != "") {
      let updatedTask: TodoTask = { ...taskToBeUpdated, description: this.taskDescription!};
      this.onUpdate.emit(updatedTask);
    }    
    this.editMode = false;
  }

  deleteTask(taskId: number) {
    this.onDelete.emit(taskId);
  }

}
