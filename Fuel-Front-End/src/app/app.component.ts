import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {AuthService} from './_services/auth.service';
import {JwtHelperService} from '@auth0/angular-jwt';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private router: Router, private authService: AuthService){}
  jwtHelper = new JwtHelperService();
  ngOnInit() {
    // Rerouting to home when refresh app or first start
    this.router.navigate(['home']);
    // If there is already a token => user is active in session
    const token = localStorage.getItem('token');
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }
}
