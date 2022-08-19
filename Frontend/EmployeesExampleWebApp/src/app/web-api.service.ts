import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Department } from './models/department';
import { Employee } from './models/employee';

@Injectable({
  providedIn: 'root'
})
export class WebAPIService {

  readonly rootURL = 'http://localhost:5221/api';
  readonly photoURL = 'http://localhost:5221/Photos';

  constructor(private http: HttpClient) { }

  //#region Department APIs

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(this.rootURL + '/Department');
  }

  addDepartment(department: Department) {
    return this.http.post(this.rootURL + '/Department', department);
  }

  updateDepartment(department: Department) {
    return this.http.put(this.rootURL + '/Department', department);
  }

  deleteDepartment(id: number) {
    return this.http.delete(this.rootURL + '/Department/' + id);
  }
  //#endregion

  //#region Employee APIs

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.rootURL + '/Employee');
  }

  addEmployee(employee: Employee) {
    return this.http.post(this.rootURL + '/Employee', employee);
  }

  updateEmployee(employee: Employee) {
    return this.http.put(this.rootURL + '/Employee', employee);
  }

  deleteEmployee(id: number) {
    return this.http.delete(this.rootURL + '/Employee/' + id);
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
