import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
data: any;
  constructor(private route: ActivatedRoute, private authService: AuthService) { }
  username = this.authService.decodedToken.unique_name;
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.data = data;
      console.log(this.data.user.quote);
    });
  }
}
