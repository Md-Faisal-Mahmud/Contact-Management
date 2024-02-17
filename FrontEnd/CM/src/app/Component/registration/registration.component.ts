import { Component } from '@angular/core';
import { IRegister } from '../../Models/register.model';
import { AuthService } from '../../Services/auth.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {

  registerData: IRegister = {
    email: '',
    password: '',
    confirmpassword: '',
    username: ''
  };

  constructor(private authService: AuthService){}
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
