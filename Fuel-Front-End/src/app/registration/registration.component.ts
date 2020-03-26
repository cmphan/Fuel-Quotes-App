import { Component, OnInit } from '@angular/core';
import {FormGroup, FormBuilder, Validators} from '@angular/forms';
import {passValidator} from './validator';
import { AuthService } from '../_services/auth.service';
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  model: any = {};
  registerFailure : boolean;
  registerSuccess : boolean;
  form: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.form = this.fb.group({
      // Minimum length of 4 characters for username 
      username: ['', Validators.minLength(5)],
      password: ['', Validators.minLength(8)],
      cnfpass: ['', passValidator],
    });
    // Subscribe to any change in password or confirmed password fields.
    this.form.controls.password.valueChanges.subscribe(x => this.form.controls.cnfpass.updateValueAndValidity());
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      this.registerSuccess = true;
      this.registerFailure = false;
    }, error => {
      this.registerFailure = true;
      this.registerSuccess = false;
    });
  }

  onSubmit() {
    this.model.username = this.form.value.username;
    this.model.password = this.form.value.password;
    this.register();
  }
  // Get username from the form
  ngOnInit() {
  }

}
