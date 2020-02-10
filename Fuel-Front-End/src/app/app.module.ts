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
import { RegisterComponent } from './register/register.component';
import { RegistrationComponent } from './registration/registration.component';
import { RegistrationComponent } from './registration/registration.component';

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
      RegisterComponent,
      RegistrationComponent,
      RegistrationComponent
   ],
   imports: [
      BrowserModule,
      BsDropdownModule.forRoot(),
      CollapseModule.forRoot(),
      BrowserAnimationsModule,
      CollapseModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
