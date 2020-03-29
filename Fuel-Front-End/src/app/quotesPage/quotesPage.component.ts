import { Component, OnInit } from '@angular/core';
import {AuthService} from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { ClientProfile } from '../_models/clientProfile';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';
import { Quote } from '../_models/quote';
import { AlertifyService } from '../_services/alertify.service';
@Component({
  selector: 'app-quotesPage',
  templateUrl: './quotesPage.component.html',
  styleUrls: ['./quotesPage.component.css']
})
export class QuotesPageComponent implements OnInit {
  constructor(private authService: AuthService,
              private userService: UserService,
              private alertify: AlertifyService,
              private route: ActivatedRoute) { }
  profile: ClientProfile;
  user: User;
  newQuote: any = { price : 0, amountDue : 0};
  newQuoteForm =  new FormData();
  loggedIn() {
    return this.authService.loggedIn();
  }
  getFullName() {
    return this.newQuote.fullname;
  }
  getQuote() {
    console.log(this.newQuote.gallon);
    console.log(this.newQuote.date);
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
      this.user = data.user;
      this.newQuote.fullname = this.user.clientProfile.fullname;
      this.newQuote.address1 = this.user.clientProfile.address1;
    });

  }

}
