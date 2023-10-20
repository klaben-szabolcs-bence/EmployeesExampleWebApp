import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Department } from './models/department';
import { Employee } from './models/employee';
import { Message } from './models/message';

@Injectable({
  providedIn: 'root'
})
export class WebAPIService {

  readonly port = 5000;
  readonly rootURL = `http://localhost:${this.port}/api`;
  readonly photoURL = `http://localhost:${this.port}/Photos`;

  constructor(private http: HttpClient) { }

  //#region Department APIs

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(this.rootURL + '/Department');
  }

  addDepartment(department: Department): Observable<Message> {
    return this.http.post<Message>(this.rootURL + '/Department', department);
  }

  updateDepartment(department: Department): Observable<Message> {
    return this.http.put<Message>(this.rootURL + '/Department', department);
  }

  deleteDepartment(id: number): Observable<Message> {
    return this.http.delete<Message>(this.rootURL + '/Department/' + id);
  }
  //#endregion

  //#region Employee APIs

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.rootURL + '/Employee');
  }

  addEmployee(employee: Employee): Observable<Message> {
    return this.http.post<Message>(this.rootURL + '/Employee', employee);
  }

  updateEmployee(employee: Employee): Observable<Message> {
    return this.http.put<Message>(this.rootURL + '/Employee', employee);
  }

  deleteEmployee(id: number): Observable<Message> {
    return this.http.delete<Message>(this.rootURL + '/Employee/' + id);
  }

  saveFile(file: File): Observable<Object> {
    const formData: FormData = new FormData();
    formData.append('PhotoFile', file);
    return this.http.post(this.photoURL, formData);
  }

  getAllDepartmentNames(): Observable<string[]> {
    return this.http.get<any[]>(this.rootURL + '/Department/GetAllDepartmentNames').pipe(
      map(x => x.map(y => y.DepartmentName)));
  }
  //#endregion
}
