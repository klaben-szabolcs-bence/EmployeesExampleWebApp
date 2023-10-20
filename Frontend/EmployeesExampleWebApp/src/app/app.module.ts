import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DepartmentComponent } from './department/department.component';
import { ShowDepartmentComponent } from './department/show/show.component';
import { AddEditDepartmentComponent } from './department/add-edit/add-edit.component';
import { EmployeeComponent } from './employee/employee.component';
import { ShowEmployeeComponent } from './employee/show/show.component';
import { AddEditEmployeeComponent } from './employee/add-edit/add-edit.component';
import { WebAPIService } from './web-api.service';

@NgModule({
  declarations: [
    AppComponent,
    DepartmentComponent,
    ShowDepartmentComponent,
    AddEditDepartmentComponent,
    EmployeeComponent,
    ShowEmployeeComponent,
    AddEditEmployeeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [WebAPIService],
  bootstrap: [AppComponent]
})
export class AppModule { }
