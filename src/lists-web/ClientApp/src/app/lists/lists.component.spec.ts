import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from '../modules/material.module';
import { RouterModule } from '@angular/router';

import { ListsComponent } from './lists.component';
import { AuthService } from '../services/auth.service';
import { ListService } from '../services/list.service';

describe('ListsComponent', () => {
  let component: ListsComponent;
  let fixture: ComponentFixture<ListsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ NoopAnimationsModule, HttpClientModule, MaterialModule, RouterModule.forRoot([]) ],
      declarations: [ ListsComponent ], providers: [AuthService, ListService]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
