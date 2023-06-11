import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TaskCategoryComponent } from '../task-category/task-category.component';
import { TodoTask, TodoTaskStatus } from '../../models/todo-task';

@Component({
  selector: 'todo-main',
  standalone: true,
  imports: [CommonModule, FormsModule, TaskCategoryComponent],
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {

  newTasks: TodoTask[] = [];
  inProgressTasks: TodoTask[] = [];
  completedTasks: TodoTask[] = [];

  taskCount: number = 0;
  taskDescription: string | null = null;

  onSubmit(taskForm: any){   
    const currentTime: Date = new Date();
    const newTask: TodoTask = {
      id: ++this.taskCount,
      description: this.taskDescription ?? "",
      status: TodoTaskStatus.New,
      createdTime: currentTime,
      updatedTime: currentTime
    };
    this.newTasks.push(newTask);
    taskForm.form.reset();
    
  }

}
