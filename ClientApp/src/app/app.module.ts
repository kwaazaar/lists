import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ListService } from './services/list.service';

import { MaterialModule } from './modules/material.module';
import { ListsComponent } from './lists/lists.component';
import { LogService } from './services/log.service';
import { ListComponent } from './list/list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ListsComponent,
    ListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }), // Alwats keep as first module in imports
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'lists', component: ListsComponent },
      { path: 'lists/:id', component: ListComponent },
    ]),
    BrowserAnimationsModule, // NoopAnimationsModule
    MaterialModule,
  ],
  providers: [ LogService, ListService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
