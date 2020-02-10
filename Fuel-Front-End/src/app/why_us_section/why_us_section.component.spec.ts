/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Why_us_sectionComponent } from './why_us_section.component';

describe('Why_us_sectionComponent', () => {
  let component: Why_us_sectionComponent;
  let fixture: ComponentFixture<Why_us_sectionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Why_us_sectionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Why_us_sectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
