import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { User } from '../_models/user';
import {map} from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  profile: any;
  baseURL = environment.apiURL;
constructor(private http: HttpClient) { }
getUser(username: string): Observable<User> {
  return this.http.get<User>(this.baseURL + 'users/' + username);
}
createProfile(username: string, model: any) {
  return this.http.post(this.baseURL + 'users/' + username + '/profile', model).
  pipe(
    map((response: any) => {
      this.profile = response;
      // If response object is not empty 
      if (Object.keys(this.profile).length > 0) {
        localStorage.setItem('fullname', this.profile.fullname);
        localStorage.setItem('address1', this.profile.address1);
        localStorage.setItem('address2', this.profile.address2);
        localStorage.setItem('city', this.profile.city);
        localStorage.setItem('state', this.profile.state);
        localStorage.setItem('zipcode', this.profile.zipcode);
        localStorage.setItem('photoURL', this.profile.photoURL);
      }
    }));
}
}

