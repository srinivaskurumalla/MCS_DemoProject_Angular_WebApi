import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { environment$ } from 'src/environments/environment.prod';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserCredentails } from '../Models/User-Credentails';
import { User } from '../Models/User';
import { Claims } from '../Models/Claims';

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



  register(user: User) : Observable<User>{
    return this._http.post<User>(`${this.apiUrl$}/Users/Register`, user);
  }

  login(userCredentails: UserCredentails): Observable<any> {
    //return this._http.post(`${this.apiUrl$}/Users/login`, data);
   return this._http.post(`${this.apiUrl$}/Users/login`, userCredentails,{ responseType: 'text' });

  }

  getAllUsers(): Observable<User[]> {
    return this._http.get<User[]>(`${this.apiUrl$}/Users/GetAllUsers`)
  }
  getAllClaims(): Observable<Claims[]> {
    return this._http.get<Claims[]>(`${this.apiUrl$}/Claims/GetAllClaims`)

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
      return `${this.userPayLoad.name}_${this.userPayLoad.lastName}`
      //return this.userPayLoad.name;
    } else {
      return "GUEST";
    }
  }

  getRoleFromToken() {
    if (this.userPayLoad) {
      return this.userPayLoad.Role;
    }
  }
}
