import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { User } from '../_models/user';
import { ClientProfile } from '../_models/clientProfile';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseURL = environment.apiURL;
  private userProfile = new Subject<ClientProfile>();
  userProfile$ = this.userProfile.asObservable();

constructor(private http: HttpClient) { }
getUser(username: string): Observable<User> {
  return this.http.get<User>(this.baseURL + 'users/' + username);
}
getUserProfile(profile: ClientProfile) {
  this.userProfile.next(profile);

}
}

