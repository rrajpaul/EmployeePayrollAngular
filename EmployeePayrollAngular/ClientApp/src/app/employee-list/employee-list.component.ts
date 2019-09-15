import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  constructor(private httpService: HttpClient) { }
  empList: string[];
  ngOnInit() {
    this.httpService.get('https://localhost:44349/api/employee').subscribe(
      data => {
        this.empList = data as string[];
      }
    );
  }

}  
