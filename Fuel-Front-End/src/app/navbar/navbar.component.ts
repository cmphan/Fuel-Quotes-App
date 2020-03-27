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
  isLogin: boolean;
  logout() {
    localStorage.removeItem('token');
    this.isLogin = false;
    this.router.navigateByUrl('/home');
  }
  ngOnInit() {
    this.authService.loginStatus$.subscribe(
      status => {
        if (status) {
          this.isLogin = true;
        } else {
          this.isLogin = false;
        }
      }
    );
  }
}
