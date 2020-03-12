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
import { FormsModule } from '@angular/forms'; 
import { HistoryComponent } from './history/history.component';
import { Routes, RouterModule } from '@angular/router'

const appRoutes: Routes =[
   {path:'history', component: HistoryComponent},
   {path:'contact', component: ContactUsComponent},
   {path:'about', component:AboutUsComponent},
   {path:'login', component:LoginComponent},
   {path:'register', component:RegistrationComponent},
   {path:'quote', component:QuotesPageComponent},
   {path:'profile', component:ProfileComponent},
   {path:'home', component:Home_pageComponent},
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
      FormsModule,
      RouterModule.forRoot(appRoutes),
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
