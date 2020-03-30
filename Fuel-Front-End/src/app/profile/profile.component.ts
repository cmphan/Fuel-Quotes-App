import { Component, OnInit } from '@angular/core';
import {FormGroup, FormBuilder, Validators} from '@angular/forms';
import { AlertifyService } from '../_services/alertify.service';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../_services/user.service';
import {map} from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  fileUploaded = false;
  profilePicDefaultURL = 'http://ssl.gstatic.com/accounts/ui/avatar_2x.png';
  model: any = {};
  profileLocalStorage: any = {};
  userProfile: any = {};
  profileCompleted: boolean;
  profileForm: FormGroup;
  fd = new FormData();
  selectedFile: File = null;
  isEdit = false;
  constructor(private formBuilder: FormBuilder, private userService: UserService,
              private alertify: AlertifyService, private router: Router,
              private authService: AuthService, private route: ActivatedRoute,
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
    this.userService.createProfile(this.authService.decodedToken.unique_name, this.fd).
    subscribe(() => {
      console.log('successfullll!!');
    }, error => {
      console.log(error);
    }
    );
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
    this.profileCompleted = true;
    this.userService.isSetPicProfile(true);
    this.alertify.success('Profile save successfully');
    this.isEdit = true;
  }
    onFileSelected(event) {
    this.selectedFile = event.target.files[0];
    this.fileUploaded = true;
  }
  clickEdit() {
    this.profileForm.enable();
    this.profileForm.markAsUntouched();
    this.isEdit = false;
  }

  ngOnInit() {
    this.userProfile.photoURL = this.profilePicDefaultURL;
    this.route.data.subscribe(data => {
      // If there is data 
      if (Object.keys(data).length > 0)
      {
        // If there is a profile associated with user
        this.userProfile.fullname = data.user.clientProfile.fullname;
        this.userProfile.address1 = data.user.clientProfile.address1;
        this.userProfile.address2 = data.user.clientProfile.address2;
        this.userProfile.city = data.user.clientProfile.city;
        this.userProfile.zipcode = data.user.clientProfile.zipcode;
        this.userProfile.state = data.user.clientProfile.state;
        this.userProfile.photoURL = data.user.clientProfile.photoURL;
        this.userService.profilePic(this.userProfile.photoURL);
        this.profileForm.disable();
        if (this.userProfile.photoURL !== this.profilePicDefaultURL) {
          this.fileUploaded = true;
        } else {
          this.fileUploaded = false;
        }
      }
    });
  }
}
