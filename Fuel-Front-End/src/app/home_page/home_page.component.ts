import { Component, OnInit } from '@angular/core';
import {LoginService} from '../login.service'; 
@Component({
  selector: 'app-home_page',
  templateUrl: './home_page.component.html',
  styleUrls: ['./home_page.component.css']
})
export class Home_pageComponent implements OnInit {
  isCollapsed: boolean;
  isHome: boolean;
  isAbout: boolean;
  isRegister: boolean;
  isContact: boolean;
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
    this.appsevice.Register.subscribe(c => {
      this.isRegister = c;
    });
    this.appsevice.Contact.subscribe(c => {
      this.isContact = c;
    });
  }
}
