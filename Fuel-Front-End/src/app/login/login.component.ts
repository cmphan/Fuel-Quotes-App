import { Component, OnInit } from '@angular/core';
import {AuthService} from '../_services/auth.service';
import {Router} from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { User } from '../_models/user';
import { ClientProfile } from '../_models/clientProfile';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  user: User;
  profile: ClientProfile;
  constructor(private authServices: AuthService,
              private router: Router,
              private alertify: AlertifyService) { }
  wrongPassword: boolean;
  ngOnInit() {
  }
  login() {
    this.authServices.login(this.model).subscribe(next => {
      this.router.navigate(['/quote', this.authServices.decodedToken.unique_name ]);
      this.authServices.checkLoginStatus(true);
      this.wrongPassword = false;
      this.alertify.success('login successfully');
    }, error => {
      this.wrongPassword = true;
    });
  }

}
