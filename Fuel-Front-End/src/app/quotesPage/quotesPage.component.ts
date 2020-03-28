import { Component, OnInit } from '@angular/core';
import {AuthService} from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { ClientProfile } from '../_models/clientProfile';
@Component({
  selector: 'app-quotesPage',
  templateUrl: './quotesPage.component.html',
  styleUrls: ['./quotesPage.component.css']
})
export class QuotesPageComponent implements OnInit {
  constructor(private authService: AuthService, private userService: UserService) { }
  public gallon: number;
  public price: number = 3;
  public total: number;
  profile: ClientProfile;
  address1: string;
  fullname: string;
  cal() {
    this.total = this.gallon * this.price;
  }
  loggedIn() {
    return this.authService.loggedIn();
  }
  getFullName() {
    return this.fullname;
  }
  ngOnInit() {
    this.userService.userProfile$.subscribe( profile => {
      // check if get user profile back
      if ((Object.keys(profile).length) > 0)
      {
        this.profile = profile;
        this.fullname = this.profile.fullname;
        this.address1 = this.profile.address1;
      }
    });
  }

}
