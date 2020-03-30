import { Component, OnInit } from '@angular/core';
import {FormGroup, FormBuilder, Validators} from '@angular/forms';
import {passValidator} from './validator';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  model: any = {};
  registerFailure: boolean;
  registerSuccess: boolean;
  form: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private alertify: AlertifyService) {
    this.form = this.fb.group({
      // Minimum length of 5 characters for username, and 8 characters for password
      username: ['', Validators.minLength(5)],
      password: ['', Validators.minLength(8)],
      cnfpass: ['', passValidator],
    });
    // Subscribe to any change in password or confirmed password fields.
    this.form.controls.password.valueChanges.subscribe(x => this.form.controls.cnfpass.updateValueAndValidity());
  }

  // Register successfully => redirect to profile => count as a successfully login
  register() {
    this.authService.register(this.model).subscribe(() => {
      this.registerSuccess = true;
      this.registerFailure = false;
      this.authService.login(this.model).subscribe(() => {
        this.router.navigate(['/profile', this.authService.decodedToken.unique_name]);
        this.alertify.success('Resigter successfully');
        // update login status after registered successfully
        this.authService.checkLoginStatus(true);
      }, error => {
        console.log(error);
      });
      // update the new registered username 
    }, error => {
      this.registerFailure = true;
      this.registerSuccess = false;
    });
  }

  onSubmit() {
    this.model.username = this.form.value.username;
    this.model.password = this.form.value.password;
    localStorage.setItem('username', this.model.username);
    this.register();
  }
  // Get username from the form
  ngOnInit() {

  }

}
