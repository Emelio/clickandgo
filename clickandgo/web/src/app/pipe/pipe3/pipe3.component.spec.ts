/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Pipe3Component } from './pipe3.component';

describe('Pipe3Component', () => {
  let component: Pipe3Component;
  let fixture: ComponentFixture<Pipe3Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Pipe3Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Pipe3Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
