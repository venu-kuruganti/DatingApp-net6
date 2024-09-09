import { Component, inject } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  http: any = inject(HttpClient);
  private toastr:any = inject(ToastrService);
  registerMode = false;
  users: any;

  ngOnInit(): void {
    this.getUsers();
  }


  registerToggle(){    
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }

  getUsers() {
    this.http.get('https://localhost:8080/API/Users').subscribe({
      next: response => {this.users = response;},
      error: error =>  this.toastr.error(error.error),
      complete: () => console.log("Request has been completed!")
    })

  }
}
