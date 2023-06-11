import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskCategoryComponent } from '../task-category/task-category.component';

@Component({
  selector: 'todo-main',
  standalone: true,
  imports: [CommonModule, TaskCategoryComponent],
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {

}
