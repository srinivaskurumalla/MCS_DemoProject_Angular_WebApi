import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { environment$ } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class DbService {
  private apiUrl: string = environment.apiUrl;
  private apiUrl$: string = environment$.apiUrl;
  constructor(private _http: HttpClient,private _router : Router) { }

  register(data: any) : Observable<any>{
    return this._http.post(`https://localhost:7037/api/Users/Register`, data);
  }

  login(data: any): Observable<any> {
    //return this._http.post(`${this.apiUrl$}/Users/login`, data);
   return this._http.post('https://localhost:7037/api/Users/login', data,{ responseType: 'json' });

  }

  getAllUsers(): Observable<any> {
    return this._http.get<any>('https://localhost:7037/api/Users/GetAllUsers')
  }
  logout() {
    localStorage.clear();
    this._router.navigate(['/login'])

  }
  storeToken(token: string) {
    localStorage.setItem('token',token)
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean{
    return !!localStorage.getItem('token');
  }
}
