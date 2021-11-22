import { Injectable } from '@angular/core';
import {  HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { catchError} from "rxjs/operators";
import {  throwError } from 'rxjs';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})

export class AuthInterceptorService implements HttpInterceptor {
private router : Router;
  constructor(router : Router){
    this.router = router;
  }
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    console.log("Interception In Progress");
    const token: string = localStorage.getItem('token') || "";
    if (token) {
      req = req.clone({
        setHeaders: {
          'Authorization': 'Bearer ' + token,
        }
      });
    }

    return next.handle(req)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if (error && error.status === 401) {
            this.router.navigate(['login']);
          }
          const err = error.error.message || error.statusText;
          return throwError(error);              
        })
      );
  }
}