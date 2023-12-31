import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TaskCategoryComponent } from '../task-category/task-category.component';
import { TodoTask, TodoTaskStatus } from '../../models/todo-task';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'todo-main',
  standalone: true,
  imports: [CommonModule, FormsModule, TaskCategoryComponent],
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  newTasks: TodoTask[] = [];
  inProgressTasks: TodoTask[] = [];
  completedTasks: TodoTask[] = [];

  taskCount: number = 0;
  taskDescription: string | null = null;
  selectedTask: TodoTask | null = null;
  todoTaskStatus = TodoTaskStatus;

  dataService: DataService = inject(DataService);

  ngOnInit(): void {
    this.getTaskList();
  }

  onSubmit(taskForm: any) {
    if (this.taskDescription && this.taskDescription.trim() != "") {
      this.dataService.addTask(this.taskDescription).subscribe({
        next: (res: TodoTask) => {
          this.newTasks.push(res);
        },
        error: error => console.error(error),
        complete: () => {
          taskForm.form.reset();
        }
      });
    }
  }

  onDelete(taskId: number) {
    this.dataService.deleteTask(taskId).subscribe({
      next: (res: TodoTask) => {
        this.getTaskList();
      },
      error: error => console.error(error),
    });
  }

  onUpdate(updatedTask: TodoTask) {
    this.dataService.updateTask(updatedTask).subscribe({
      next: (res: TodoTask) => {
        this.getTaskList();
      },
      error: error => console.error(error),
    });
  }

  onSelectedTaskChange(task: TodoTask) {
    this.selectedTask = task;
  }

  getTaskList() {
    this.dataService.getTasks().subscribe((tasks: TodoTask[]) => {
      this.mapTasks(tasks);
      this.updateSelectedTask(tasks);
    });
  }

  mapTasks(tasks: TodoTask[]) {
    this.newTasks = tasks.filter(task => task.status === TodoTaskStatus.New);
    this.inProgressTasks = tasks.filter(task => task.status === TodoTaskStatus.InProgress);
    this.completedTasks = tasks.filter(task => task.status === TodoTaskStatus.Completed);
  }

  updateSelectedTask(tasks: TodoTask[]) {
    if (this.selectedTask) {
      this.selectedTask = tasks.find(task => task.id == this.selectedTask!.id) ?? null;
    }
  }

  updateTaskStatus(taskStatus: TodoTaskStatus) {
    let taskToBeUpdated: TodoTask = { ...this.selectedTask!, status: taskStatus };
    this.onUpdate(taskToBeUpdated);
  }

}
