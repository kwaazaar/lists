import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from '../modules/material.module';
import { RouterModule } from '@angular/router';

import { PlayListComponent } from './play-list.component';

describe('PlayListComponent', () => {
  let component: PlayListComponent;
  let fixture: ComponentFixture<PlayListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ NoopAnimationsModule, HttpClientModule, MaterialModule, RouterModule.forRoot([]) ],
      declarations: [ PlayListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
