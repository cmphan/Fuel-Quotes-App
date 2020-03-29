import { Component, OnInit } from '@angular/core';
import {FormGroup, FormBuilder, Validators} from '@angular/forms';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../_services/user.service';
import {map} from 'rxjs/operators';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  model: any = {};
  profileLocalStorage: any = {};
  profileCompleted: boolean;
  profileForm: FormGroup;
  fd = new FormData();
  selectedFile: File = null;
  constructor(private formBuilder: FormBuilder, private userService: UserService,
              private alertify: AlertifyService, private router: Router,
              private http: HttpClient) {
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
    this.userService.createProfile('clayton', this.fd).
    subscribe(() => {
      console.log('successfullll!!');
    }, error => {
      console.log(error);
    }
    );
  }
  getProfile() {
    this.profileLocalStorage.fullname = localStorage.getItem('fullname');
    this.profileLocalStorage.address1 = localStorage.getItem('address1');
    this.profileLocalStorage.address2 = localStorage.getItem('address2');
    this.profileLocalStorage.city = localStorage.getItem('city');
    this.profileLocalStorage.state = localStorage.getItem('state');
    this.profileLocalStorage.zipcode = localStorage.getItem('zipcode');
    this.profileLocalStorage.photoURL = localStorage.getItem('photoURL');
    localStorage.removeItem('fullname');
    localStorage.removeItem('address1');
    localStorage.removeItem('address2');
    localStorage.removeItem('city');
    localStorage.removeItem('state');
    localStorage.removeItem('zipcode');
    localStorage.removeItem('photoURL');
    console.log(this.profileLocalStorage);
  }
  onSubmit() {
    this.fd.append('File', this.selectedFile, this.selectedFile.name);
    this.fd.append('fullname', this.profileForm.value.fullname);
    this.fd.append('address1', this.profileForm.value.address1);
    this.fd.append('address2', this.profileForm.value.address2);
    this.fd.append('city', this.profileForm.value.city);
    this.fd.append('state', this.profileForm.value.state);
    this.fd.append('zipcode', this.profileForm.value.zipcode);
    this.profileForm.markAllAsTouched();
    this.profile();
    this.getProfile();
    this.profileCompleted = true;
  }
    onFileSelected(event) {
    this.selectedFile = event.target.files[0];
  }

  ngOnInit() {
  }
}
