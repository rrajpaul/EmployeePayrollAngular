import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";

@Injectable()
export class EmployeeService {
  private url = "https://localhost:44349/api/employee";

  constructor(private http: HttpClient) { }

  getEmployees(): Observable<any> {
    return this.http.get<any>(this.url);
  }
}
