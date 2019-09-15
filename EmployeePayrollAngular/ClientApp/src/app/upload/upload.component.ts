import { Component } from "@angular/core";
import { Router } from '@angular/router';
import {
  HttpClient,
  HttpRequest,
  HttpEventType,
  HttpResponse
} from "@angular/common/http";
import { Console } from "@angular/core/src/console";

@Component({
  selector: "upload-component",
  templateUrl: "./upload.component.html"
})
export class UploadComponent {
  public progress: number;
  public message: string;
  constructor(private http: HttpClient) { }
  private router: Router;


  upload(files) {
    if (files.length === 0) return;

    const formData = new FormData();

    for (let file of files) formData.append(file.name, file);

    const uploadReq = new HttpRequest("POST", 'api/upload', formData, {
      reportProgress: true, responseType: "text"
    });

    this.http.request(uploadReq).subscribe(event => {     
      if (event.type === HttpEventType.UploadProgress) {  
        this.progress = Math.round((100 * event.loaded) / event.total);
      } else if (event.type === HttpEventType.Response) {  
        this.message = event.body.toString();
        if (this.message != "OK") {
          console.log(this.message);
        } else {          
          setTimeout(() => {
            this.router.navigate(['/employee']);
          }, 500);
        }
      }              
    });
   // this.router.navigate(['employee']);
  }
}
