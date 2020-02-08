import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CollapseModule, BsDropdownModule } from 'ngx-bootstrap';
import { NavbarComponent } from './navbar/navbar.component';
import { Hero_imageComponent } from './hero_image/hero_image.component';

@NgModule({
   declarations: [
      AppComponent,
      NavbarComponent,
      Hero_imageComponent
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
