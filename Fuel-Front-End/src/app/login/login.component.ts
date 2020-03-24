import { Component, OnInit } from '@angular/core';
import {AuthService} from '../_services/auth.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  constructor(private authServices: AuthService) { }

  ngOnInit() {
  }
  login() {
    this.authServices.login(this.model).subscribe(next => {
      console.log('login successfully');
    }, error => {
      console.log('Failed to login');
    });
  }

}
