import { Component, OnInit } from '@angular/core';
import {AuthService} from '../_services/auth.service';
import {Router} from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  constructor(private authServices: AuthService, private router: Router, private alertify: AlertifyService) { }
  wrongPassword: boolean;
  ngOnInit() {
  }
  login() {
    this.authServices.login(this.model).subscribe(next => {
      this.router.navigateByUrl('/quote');
      this.wrongPassword = false;
      this.alertify.success('login successfully');
    }, error => {
      this.wrongPassword = true;
      console.log('Failed to login');
    });
  }

}
