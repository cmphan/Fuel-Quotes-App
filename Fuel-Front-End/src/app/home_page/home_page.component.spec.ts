/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Home_pageComponent } from './home_page.component';

describe('Home_pageComponent', () => {
  let component: Home_pageComponent;
  let fixture: ComponentFixture<Home_pageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Home_pageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Home_pageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
