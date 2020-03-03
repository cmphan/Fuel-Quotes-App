import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
isLogin = false;
isHome = true;
isAbout = false;
isRegister = false;
isContact = false;
isProfile = false;
Register: BehaviorSubject<boolean>;
Collapsed: BehaviorSubject<boolean>;
Home: BehaviorSubject<boolean>;
About: BehaviorSubject<boolean>;
Contact: BehaviorSubject<boolean>;
Profile: BehaviorSubject<boolean>;
constructor() {
this.Collapsed  = new BehaviorSubject(this.isLogin);
this.Home = new BehaviorSubject(this.isHome);
this.About = new BehaviorSubject(this.isAbout);
this.Register = new BehaviorSubject(this.isRegister);
this.Contact = new BehaviorSubject(this.isContact);
this.Profile = new BehaviorSubject(this.isProfile);
}
loginCollapsed() {
  /* Change the value of login collapsed -> change to login module
  ================================================================
    logic: check if in home page  -> yes -> isHome = true , mean not in Login page & everything else => isLogin = false
           check if in log in page -> yes -> isHome = false && isLogin = true -> back to home page => isHome = true && isLogin = false
  */
    if (this.isLogin === false) 
    {this.isLogin = true; this.isHome = false; this.isAbout = false; this.isRegister = false; this.isContact = false; this.isProfile = false;}
    this.Collapsed.next(this.isLogin);
    this.Home.next(this.isHome);
    this.About.next(this.isAbout);
    this.Register.next(this.isRegister);
    this.Contact.next(this.isContact);
    this.Profile.next(this.isProfile);
  }
homePage() {
  if (this.isHome === false) 
  {this.isHome = true; this.isLogin = false; this.isAbout = false; this.isRegister = false; this.isContact = false; this.isProfile = false;}
  this.Home.next(this.isHome);
  this.Collapsed.next(this.isLogin);
  this.About.next(this.isAbout);
  this.Register.next(this.isRegister);
  this.Contact.next(this.isContact);
  this.Profile.next(this.isProfile);
}
aboutPage() {
  if (this.isAbout === false) 
  {this.isAbout = true; this.isHome = false; this.isLogin = false; this.isRegister = false; this.isContact = false; this.isProfile = false;}
  this.Home.next(this.isHome);
  this.About.next(this.isAbout);
  this.Collapsed.next(this.isLogin);
  this.Register.next(this.isRegister);
  this.Contact.next(this.isContact);
  this.Profile.next(this.isProfile);
}
registerPage() {
  if (this.isRegister === false) 
  {this.isRegister = true; this.isHome = false; this.isLogin = false; this.isAbout = false; this.isContact = false; this.isProfile = false;}
  this.Home.next(this.isHome);
  this.Collapsed.next(this.isLogin);
  this.About.next(this.isAbout);
  this.Register.next(this.isRegister);
  this.Contact.next(this.isContact);
  this.Profile.next(this.isProfile);
}
contactPage() {
  if (this.isContact === false) 
  {this.isContact = true; this.isLogin = false; this.isAbout = false; this.isRegister = false; this.isHome = false; this.isProfile = false;}
  this.Home.next(this.isHome);
  this.Collapsed.next(this.isLogin);
  this.About.next(this.isAbout);
  this.Register.next(this.isRegister);
  this.Contact.next(this.isContact);
  this.Profile.next(this.isProfile);
}
profilePage() {
  if(this.isProfile === false)
  {this.isProfile = true, this.isContact = false; this.isLogin = false; this.isAbout = false; this.isRegister = false; this.isHome = false; }
  this.Home.next(this.isHome);
  this.Collapsed.next(this.isLogin);
  this.About.next(this.isAbout);
  this.Register.next(this.isRegister);
  this.Contact.next(this.isContact);
  this.Profile.next(this.isProfile);
}
}
