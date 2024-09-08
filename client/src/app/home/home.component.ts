import { Component, inject } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  http: any = inject(HttpClient);
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
      next: response => {this.users = response; console.log(response);},
      error: error => console.log(error),
      complete: () => console.log("Request has been completed!")
    })

  }
}
