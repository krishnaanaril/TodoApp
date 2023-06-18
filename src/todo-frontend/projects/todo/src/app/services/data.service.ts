import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TodoTask } from '../models/todo-task';
import { Observable } from 'rxjs';

const API_URL = "https://localhost:7060/TodoTask";

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  getTasks() : Observable<TodoTask[]>{
    const url = `${API_URL}/Get`;
    return this.http.get<TodoTask[]>(url);
  }

  getTaskById(id: number): Observable<TodoTask> {
    const url = `${API_URL}/GetById`;
    const options = { params: new HttpParams().set('id', id)};
    return this.http.get<TodoTask>(url, options);
  }

  addTask(taskDescription: string) {
    const url = `${API_URL}/AddTask`;
    return this.http.post<TodoTask>(url, taskDescription);
  }

  updateTask(updatedTask: TodoTask){
    const url = `${API_URL}/UpdateTask`;
    return this.http.put<TodoTask>(url, updatedTask);
  }

  deleteTask(id: number) {
    const url = `${API_URL}/DeleteTask`;
    const options = { params: new HttpParams().set('id', id)};
    return this.http.delete<TodoTask>(url, options);
  }
}
