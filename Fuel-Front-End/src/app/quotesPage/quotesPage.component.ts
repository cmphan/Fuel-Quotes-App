import { Component, OnInit } from '@angular/core';
import {AuthService} from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { ClientProfile } from '../_models/clientProfile';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../_models/user';
import { Quote } from '../_models/quote';
import { AlertifyService } from '../_services/alertify.service';
import { PlatformLocation } from '@angular/common'
@Component({
  selector: 'app-quotesPage',
  templateUrl: './quotesPage.component.html',
  styleUrls: ['./quotesPage.component.css']
})
export class QuotesPageComponent implements OnInit {
  constructor(private authService: AuthService,
              private userService: UserService,
              private alertify: AlertifyService,
              private route: ActivatedRoute,
              private location: PlatformLocation,
              private router: Router) {
              location.onPopState(() => { 
                // Detect a backward click on brower and change status to log out 
                this.authService.checkLoginStatus(false);
                localStorage.removeItem('token');
                this.router.navigateByUrl('/home');
              });
               }
  profile: ClientProfile;
  user: User;
  newQuote: any = { price : 0, amountDue : 0};
  prevQuote: any = {};
  newQuoteForm =  new FormData();
  loggedIn() {
    return this.authService.loggedIn();
  }
  getFullName() {
    return this.newQuote.fullname;
  }
  getQuote() {
    this.newQuoteForm.append('GallonsRequested', this.newQuote.gallon);
    this.newQuoteForm.append('DeliveryAddress', this.newQuote.address1);
    this.newQuoteForm.append('DeliveryDate', this.newQuote.date);
    this.userService.getQuote(this.authService.decodedToken.unique_name, this.newQuoteForm).subscribe(() => {
    this.newQuote.price = localStorage.getItem('suggestedPrice');
    this.newQuote.amountDue = localStorage.getItem('amountDue');
    this.alertify.success('Quote Succesfully');
    localStorage.removeItem('suggestedPrice');
    localStorage.removeItem('amountDue');
    }, error => {
      console.log(error);
    })
  }
  ngOnInit() {
      this.route.data.subscribe(data => {
        // if client profile is empty => new user => redirect to client profile to complete
        // before they can start the first quote
      if (data.user['clientProfile'] !== null) {
      console.log(data.user);
      this.user = data.user;
      this.newQuote.fullname = this.user.clientProfile.fullname;
      this.newQuote.address1 = this.user.clientProfile.address1 + ', ' +
      this.user.clientProfile.city + ', ' + this.user.clientProfile.state + ' ' +  this.user.clientProfile.zipcode;
      this.userService.profilePic(this.user.clientProfile.photoURL);
      this.userService.isSetPicProfile(true);
      this.getFullName();
      } else {
        this.router.navigate(['/profile', this.authService.decodedToken.unique_name]);
       }
    });
  }

}
