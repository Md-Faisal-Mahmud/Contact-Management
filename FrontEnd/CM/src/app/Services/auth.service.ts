import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IRegister } from '../Models/register.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { 

  }
  apiUrl = 'https://localhost:7061'
  
  registerUser(registerData: IRegister): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, registerData); 
  }

}
