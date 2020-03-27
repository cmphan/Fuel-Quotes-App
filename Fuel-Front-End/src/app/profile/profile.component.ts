import { Component, OnInit } from '@angular/core';
import {FormGroup, FormBuilder, Validators} from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  model: any = {};
  profileCompleted: boolean;
  profileForm: FormGroup;
  constructor(private formBuilder: FormBuilder, private authService: AuthService,
              private alertify: AlertifyService, private router: Router) {
    this.profileForm = this.formBuilder.group({
      fullname: ['', [Validators.required]],
      address1: ['', [Validators.required]],
      address2: [''],
      city: ['', [Validators.required]],
      state: ['', [Validators.required]],
      zipcode: ['', [Validators.required]],

    });
  }
  profile() {
    this.authService.profile(this.model).subscribe(() => {
      // Should remove username here
      this.alertify.success('profile created successfully');
      this.router.navigateByUrl('/quote');
      localStorage.removeItem('username');
    }, error => {
      console.log(error);
    });

  }
  onSubmit() {
    this.model.username = localStorage.getItem('username');
    this.model.fullname = this.profileForm.value.fullname;
    localStorage.setItem('fullname', this.model.fullname);
    this.model.address1 = this.profileForm.value.address1;
    this.model.address2 = this.profileForm.value.address2;
    this.model.city = this.profileForm.value.city;
    this.model.state = this.profileForm.value.state;
    this.model.zipcode = this.profileForm.value.zipcode;
    console.log(this.model);
    this.profileForm.markAllAsTouched();
    this.profile();
  }

  ngOnInit() {
  }
}
