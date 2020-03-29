import { Component, OnInit } from '@angular/core';
import {AuthService} from '../_services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
  constructor(private authService: AuthService,
              private router: Router,
              private alertify: AlertifyService,
              private userSerive: UserService) {
  }
  username: any;
  isLogin = false;
  hasProfilePic = false;
  profilePicUrlUploaded: string;
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
    this.userSerive.profilePicURL$.subscribe(
      profilePicLink => {
        this.profilePicUrlUploaded = profilePicLink;
      }
    );
    this.userSerive.isSetProfilePic$.subscribe (
      isSetPicProfile => {
        this.hasProfilePic = isSetPicProfile;
      }
    );
  }
}
