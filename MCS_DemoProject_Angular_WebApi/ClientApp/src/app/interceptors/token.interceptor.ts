import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, empty, Observable, throwError } from 'rxjs';
import { DbService } from '../services/db.service';
import { Router } from '@angular/router';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private _dbService : DbService,private _router : Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    const token = this._dbService.getToken();
    if (token) {
      request =  request.clone({
        setHeaders : {Authorization : `Bearer ${token}`}
      })
    }
    return next.handle(request).pipe(
      catchError((err: any) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status === 401) {
            alert('token expired , please login again');
            this._router.navigate(['login']);
          }else {
            // Handle other errors or display a generic error message
           // console.error('HTTP error:', err.error);
            alert(err.error)
            console.log(err)
            console.log(err.error)
            // You can show a generic error message to the user here
          }
        }
        return empty();
      })

    );
  }
}
