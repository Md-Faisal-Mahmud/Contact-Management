import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IRegister } from '../Models/IRegister.model';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators'; // Import map and tap operators

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7061'; 

  constructor(private http: HttpClient) { }

  registerUser(registerData: IRegister): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/auth/register`, registerData); 
  }

  loginUser(credentials: { email: string, password: string }): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/auth/login`, credentials)
      // .pipe(
      //   // Extract the token from the response
      //   map(response => response.token),
      //   tap(token => {
      //     // Store the token in local storage
      //     localStorage.setItem('token', token);
      //   })
      // );
  }
}
