import { Component } from '@angular/core';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.css'
})
export class SigninComponent {
  signInData = {
    email: '',
    password: ''
  };

  constructor(private authService: AuthService) {}
  
  signIn() {
    this.authService.loginUser(this.signInData).subscribe({
      next: response => {
        // Handle successful sign-in
        console.log('User signed in successfully:', response);
        localStorage.setItem('token', response.data.token);
        console.log(response.data.token)
      },
      error: error => {
        // Handle sign-in error
        console.error('Error signing in:', error);
        // Optionally, you can display an error message to the user
      }
    });
  }
  
}
