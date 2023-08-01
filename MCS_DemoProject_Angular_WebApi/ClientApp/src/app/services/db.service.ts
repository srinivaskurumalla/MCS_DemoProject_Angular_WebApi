import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { environment$ } from 'src/environments/environment.prod';
import { BehaviorSubject } from "rxjs";
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class DbService {
  private apiUrl: string = environment.apiUrl;
  private apiUrl$: string = environment$.apiUrl;
  private userPayLoad: any;
  constructor(private _http: HttpClient, private _router: Router)
  {
    this.userPayLoad = this.decodedToken();
    //console.log('payload', this.userPayLoad)

   }



  register(data: any) : Observable<any>{
    return this._http.post(`https://localhost:7037/api/Users/Register`, data);
  }

  login(data: any): Observable<any> {
    //return this._http.post(`${this.apiUrl$}/Users/login`, data);
   return this._http.post('https://localhost:7037/api/Users/login', data,{ responseType: 'text' });

  }

  getAllUsers(): Observable<any> {
    return this._http.get<any>('https://localhost:7037/api/Users/GetAllUsers')
  }
  logout() {
    localStorage.clear();
    this.userPayLoad = ''
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

  decodedToken() {
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    console.log('decoded token',jwtHelper.decodeToken(token));
    return jwtHelper.decodeToken(token);
  }

  getFullNameFromToken() {
    if (this.userPayLoad) {
      return this.userPayLoad.name;
    }
  }
}
