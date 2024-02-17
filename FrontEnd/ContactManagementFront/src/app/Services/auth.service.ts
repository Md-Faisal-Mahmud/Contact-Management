import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IRegister } from '../Models/IRegister.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7061'; 

  constructor(private http: HttpClient) { }

  registerUser(registerData: IRegister): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/auth/register`, registerData); 
  }
}
