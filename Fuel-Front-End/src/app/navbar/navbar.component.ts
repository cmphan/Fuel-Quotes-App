import { Component, OnInit } from '@angular/core';
import {LoginService} from '../login.service'; 

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
  isCollapsed: boolean;
  isAbout: boolean;
  navbarOpen = false;
  isHome: boolean;
  constructor(private appsevice: LoginService) {
  }
  ngOnInit() {
    this.appsevice.Collapsed.subscribe(c => {
      this.isCollapsed = c;
    });
    this.appsevice.Home.subscribe(c => {
      this.isHome = c;
    });
    this.appsevice.About.subscribe(c => {
      this.isAbout = c;
    });
  }
  loginCollapsed() {
    this.appsevice.loginCollapsed();
  }
  homePage() {
    this.appsevice.homePage();
  }
  aboutPage() {
    this.appsevice.aboutPage();
  }
  toggleNavbar() {
    this.navbarOpen = !this.navbarOpen;
  }
}
