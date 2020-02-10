import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
isLogin = false;
isHome = true;
isAbout = false;
Collapsed: BehaviorSubject<boolean>;
Home: BehaviorSubject<boolean>;
About: BehaviorSubject<boolean>;
constructor() {
this.Collapsed  = new BehaviorSubject(this.isLogin);
this.Home = new BehaviorSubject(this.isHome);
this.About = new BehaviorSubject(this.isAbout);
}
loginCollapsed() {
  /* Change the value of login collapsed -> change to login module
  ================================================================
    logic: check if in home page  -> yes -> isHome = true , mean not in Login page & everything else => isLogin = false
           check if in log in page -> yes -> isHome = false && isLogin = true -> back to home page => isHome = true && isLogin = false
  */
    if (this.isLogin === false) { this.isLogin = true; this.isHome = false; this.isAbout = false; }
    this.Collapsed.next(this.isLogin);
    this.Home.next(this.isHome);
    this.About.next(this.isAbout);
  }
homePage() {
  if (this.isHome === false) {this.isHome = true; this.isLogin = false; this.isAbout = false; }
  this.Home.next(this.isHome);
  this.Collapsed.next(this.isLogin);
}
aboutPage() {
  if (this.isAbout === false) {this.isAbout = true; this.isHome = false; this.isLogin = false;}
  this.Home.next(this.isHome);
  this.About.next(this.isAbout);
  this.Collapsed.next(this.isLogin);
}
}
