import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt';
import {Subject} from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
baseUrl = 'http://localhost:5000/api/auth/';
jwtHelper = new JwtHelperService();
decodedToken: any;
private loginStatus = new Subject<boolean>();
loginStatus$ = this.loginStatus.asObservable();
constructor(private http: HttpClient) {
}
  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).
    pipe(
      map((response: any) => {
        const user = response;
        // if user exists
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
        }
      })
    );
  }
  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }
  profile(model: any) {
    return this.http.post(this.baseUrl + 'profile', model);
  }
  // check if token is expired
  loggedIn() {
    const token = localStorage.getItem('token');
    if (!this.jwtHelper.isTokenExpired(token) || localStorage.getItem('fullname') !== null) {
      return true;
    } else {
     return false;
    }
  }
  checkLoginStatus(status: boolean) {
    this.loginStatus.next(status);
  }
}
