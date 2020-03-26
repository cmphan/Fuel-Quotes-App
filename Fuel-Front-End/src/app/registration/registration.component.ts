import { Component, OnInit } from '@angular/core';
import {FormGroup, FormBuilder, Validators} from '@angular/forms';
import {passValidator} from './validator';
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  model: any = {};
  form: FormGroup;
  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      // Minimum length of 4 characters for username 
      username: ['', Validators.minLength(5)],
      password: ['', Validators.minLength(8)],
      cnfpass: ['', passValidator],
    });
    // Subscribe to any change in password or confirmed password fields.
    this.form.controls.password.valueChanges.subscribe(x => this.form.controls.cnfpass.updateValueAndValidity());
  }

  onSubmit(){
    console.log(this.form.value);
  }
  // Get username from the form
  ngOnInit() {
  }

}
