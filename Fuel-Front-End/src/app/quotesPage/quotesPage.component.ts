import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-quotesPage',
  templateUrl: './quotesPage.component.html',
  styleUrls: ['./quotesPage.component.css']
})
export class QuotesPageComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  public gallon:number;
  public price:number = 3;
  public total:number;

  cal(){
    this.total=this.gallon*this.price;
  }
}
