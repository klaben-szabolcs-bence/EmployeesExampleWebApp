import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Employees Example Web App';
  subtitle = 'Employee Managment Portal';
  subsubtitle = 'MS-SQL + ASP.NET Core + Angular';

  constructor(public router: Router) { }
}
