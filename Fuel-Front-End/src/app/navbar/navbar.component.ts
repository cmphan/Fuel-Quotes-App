import { Component, OnInit } from '@angular/core';
import {AuthService} from '../_services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
  constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService) {
  }
  username: any;
  isLogin = false;
  logout() {
    localStorage.removeItem('token');
    this.isLogin = false;
  }
  ngOnInit() {
    this.authService.loginStatus$.subscribe(
      status => {
        if (status) {
          // If login is status then get username 
          this.isLogin = true;
          this.username = this.authService.decodedToken.unique_name;
        } else {
          this.isLogin = false;
        }
      }
    );
  }
}
