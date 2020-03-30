import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CollapseModule, BsDropdownModule } from 'ngx-bootstrap';
import { NavbarComponent } from './navbar/navbar.component';
import { Hero_imageComponent } from './hero_image/hero_image.component';
import { LoginComponent } from './login/login.component';
import { Home_pageComponent } from './home_page/home_page.component';
import { Why_us_sectionComponent } from './why_us_section/why_us_section.component';
import { Fueling_innovation_sectionComponent } from './fueling_innovation_section/fueling_innovation_section.component';
import { FooterComponent } from './footer/footer.component';
import { AboutUsComponent } from './aboutUs/aboutUs.component';
import { CardsComponent } from './cards/cards.component';
import { RegistrationComponent } from './registration/registration.component';
import { ContactUsComponent } from './contactUs/contactUs.component';
import { ProfileComponent } from './profile/profile.component';
import { QuotesPageComponent } from './quotesPage/quotesPage.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms'; 
import { HistoryComponent } from './history/history.component';
import { Routes, RouterModule } from '@angular/router';
import { AuthService } from './_services/auth.service';
import {HttpClientModule} from '@angular/common/http';
import { AuthGuard } from './_guards/auth.guard';
import { JwtModule } from '@auth0/angular-jwt';
import { AlertifyService } from './_services/alertify.service';
import { UserService } from './_services/user.service';
import { UserDetailResolver } from './_resolvers/user-detail.resolver';

export function tokenGetter() {
   return localStorage.getItem('token');
}
const appRoutes: Routes =[
   {path:'history/:username', component: HistoryComponent, resolve: {user: UserDetailResolver}, canActivate: [AuthGuard]},
   {path:'contact', component: ContactUsComponent},
   {path: 'history', component: HistoryComponent},
   {path: 'profile', component: ProfileComponent},
   {path:'about', component:AboutUsComponent},
   {path:'login', component:LoginComponent},
   {path:'register', component:RegistrationComponent},
   {path:'quote/:username', component:QuotesPageComponent, resolve: {user: UserDetailResolver}, canActivate: [AuthGuard]},
   {path:'profile/:username', component:ProfileComponent, resolve: {user: UserDetailResolver}},
   {path:'home', component:Home_pageComponent},
   {path: '',component:Home_pageComponent},
   // Random route redirect to homepage
   {path: '**', redirectTo: '',pathMatch: 'full'}, 
];
@NgModule({
   declarations: [
      AppComponent,
      NavbarComponent,
      Hero_imageComponent,
      LoginComponent,
      Home_pageComponent,
      Why_us_sectionComponent,
      Fueling_innovation_sectionComponent,
      FooterComponent,
      AboutUsComponent,
      CardsComponent,
      RegistrationComponent,
      ContactUsComponent,
      ProfileComponent,
      QuotesPageComponent,
      HistoryComponent
   ],
   imports: [
      BrowserModule,
      BsDropdownModule.forRoot(),
      CollapseModule.forRoot(),
      BrowserAnimationsModule,
      CollapseModule,
      ReactiveFormsModule,
      FormsModule,
      RouterModule.forRoot(appRoutes),
      HttpClientModule,
      JwtModule.forRoot({
         config: {
            tokenGetter: tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      })
   ],
   providers: [
      AuthService,
      AlertifyService,
      AuthGuard,
      UserService,
      UserDetailResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
