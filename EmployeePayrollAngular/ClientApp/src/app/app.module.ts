import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { HomeComponent } from "./home/home.component";
import { UploadComponent } from "./upload/upload.component";
import { EmployeeListComponent } from "./employee-list/employee-list.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

const routes: Routes = [
  { path: "home", component: HomeComponent },
  { path: "upload", component: UploadComponent },
  { path: "employee", component: EmployeeListComponent },
  { path: "", component: HomeComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    UploadComponent,
    EmployeeListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
  ],
  exports: [RouterModule],
  providers: [], 
  bootstrap: [AppComponent]
})
export class AppModule {}
