import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TodoTask } from '../../models/todo-task';

@Component({
  selector: 'todo-task-category',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './task-category.component.html',
  styleUrls: ['./task-category.component.css']
})
export class TaskCategoryComponent {

  @Input() taskList : TodoTask[] = [];

}
