import { Component, EventEmitter, Input, inject, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TodoTask } from '../../models/todo-task';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'todo-task-category',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './task-category.component.html',
  styleUrls: ['./task-category.component.css']
})
export class TaskCategoryComponent {

  @Input() taskList : TodoTask[] = [];
  @Output() onUpdate = new EventEmitter<TodoTask>();
  @Output() onDelete = new EventEmitter<number>();
  @Output() onSelectedTaskChange = new EventEmitter<number>();
  editMode: boolean = false;
  selectedTaskId: number = -1;

  dataService: DataService = inject(DataService);

  taskTrackBy(index: number, task: TodoTask) {
    return task.id;
  }

  changeSelectedTask(taskId: number) {
    this.selectedTaskId = taskId;
    this.onSelectedTaskChange.emit(this.selectedTaskId);
  }

  updateTask(taskId: number, taskDescription: string) {
    let taskToBeUpdated = this.taskList.find(task => task.id == taskId);
    if(taskToBeUpdated) {
      let updatedTask: TodoTask = { ...taskToBeUpdated, description: taskDescription};
      this.onUpdate.emit(updatedTask);
    }    
  }

  deleteTask(taskId: number) {
    this.onDelete.emit(taskId);
  }

}
