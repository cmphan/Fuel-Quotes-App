import { Component, OnInit } from '@angular/core';
import {AuthService} from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { ClientProfile } from '../_models/clientProfile';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';
import { Quote } from '../_models/quote';
@Component({
  selector: 'app-quotesPage',
  templateUrl: './quotesPage.component.html',
  styleUrls: ['./quotesPage.component.css']
})
export class QuotesPageComponent implements OnInit {
  constructor(private authService: AuthService,
              private userService: UserService,
              private route: ActivatedRoute) { }
  public gallon: number;
  public price: number = 3;
  public total: number;
  profile: ClientProfile;
  quotes: Quote[];
  user: User;
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
      this.route.data.subscribe(data => {
      this.user = data.user;
      this.fullname = this.user.clientProfile.fullname;
      this.address1 = this.user.clientProfile.address1;
    });

  }

}
