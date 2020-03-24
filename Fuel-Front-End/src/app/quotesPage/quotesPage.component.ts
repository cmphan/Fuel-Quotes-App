import { Component, OnInit } from '@angular/core';
import {AuthService} from '../_services/auth.service';
@Component({
  selector: 'app-quotesPage',
  templateUrl: './quotesPage.component.html',
  styleUrls: ['./quotesPage.component.css']
})
export class QuotesPageComponent implements OnInit {

  constructor(public authService: AuthService) { }

  ngOnInit() {
  }

  public gallon:number;
  public price:number = 3;
  public total:number;

  cal(){
    this.total = this.gallon * this.price;
  }
  loggedIn() {
    return this.authService.loggedIn();
  }
  logout() {
    localStorage.removeItem('token');
    console.log('logged out');
  }

}
