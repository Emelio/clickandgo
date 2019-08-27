/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Pipe1Component } from './pipe1.component';

describe('Pipe1Component', () => {
  let component: Pipe1Component;
  let fixture: ComponentFixture<Pipe1Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Pipe1Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Pipe1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
