import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  private apiUrl = 'https://localhost:7061'; 

  constructor(private http: HttpClient) { }

  addContact(contactData: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/contact`, contactData);
  }

}
