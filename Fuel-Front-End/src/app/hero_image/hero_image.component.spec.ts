/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Hero_imageComponent } from './hero_image.component';

describe('Hero_imageComponent', () => {
  let component: Hero_imageComponent;
  let fixture: ComponentFixture<Hero_imageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Hero_imageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Hero_imageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
