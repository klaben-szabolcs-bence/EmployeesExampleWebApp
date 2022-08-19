import { Component, OnInit } from '@angular/core';
import { Department } from 'src/app/models/department';
import { WebAPIService } from 'src/app/web-api.service';

@Component({
  selector: 'app-show-department',
  templateUrl: './show.component.html',
  styleUrls: ['./show.component.css']
})
export class ShowDepartmentComponent implements OnInit {

  constructor(private service: WebAPIService) { }

  departmentList: Department[] = [];

  ngOnInit(): void {
    this.refreshDepartmentList();
  }

  refreshDepartmentList() {
    this.service.getDepartments().subscribe(
      result => {
        this.departmentList = result;
      }
    );
  }

  onEdit(department: Department) {
    console.log('Edit ' + department.DepartmentID);
  }

  onDelete(department: Department) {
    console.log('Delete' + department.DepartmentID);
  }

}
