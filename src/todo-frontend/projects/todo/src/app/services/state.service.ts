import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class StateService {

  selectedTask = new BehaviorSubject(0);

  constructor() { }

  setSelectedTask(taskId: number) {
    this.selectedTask.next(taskId);
  }
}
