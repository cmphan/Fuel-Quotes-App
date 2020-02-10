/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Fueling_innovation_sectionComponent } from './fueling_innovation_section.component';

describe('Fueling_innovation_sectionComponent', () => {
  let component: Fueling_innovation_sectionComponent;
  let fixture: ComponentFixture<Fueling_innovation_sectionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Fueling_innovation_sectionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Fueling_innovation_sectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
