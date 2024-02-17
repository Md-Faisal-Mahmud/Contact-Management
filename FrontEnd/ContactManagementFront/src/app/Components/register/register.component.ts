import { Component } from '@angular/core';
import { AuthService } from '../../Services/auth.service';
import { IRegister } from '../../Models/IRegister.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerData: IRegister = {
    email: '',
    password: '',
    confirmPassword: '',
    username: ''
  };

  constructor(private authService: AuthService) { }

  registerUser() {
    this.authService.registerUser(this.registerData).subscribe({
      next: () => {
        console.log('User registered successfully');
        // Handle success (e.g., navigate to another page)
      },
      error: (error) => {
        console.error('Error registering user:', error);
        // Handle error (e.g., display error message)
      }
    });
  }
}
