import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './modules/material.module';

import { ListService } from './services/list.service';
import { LogService } from './services/log.service';
import { AuthService } from './services/auth.service';

import { HomeComponent } from './home/home.component';
import { CallbackComponent } from './callback/callback.component';
import { TechdetailsComponent } from './techdetails/techdetails.component';
import { ListsComponent } from './lists/lists.component';
import { ListComponent } from './list/list.component';
import { PlayListComponent } from './play-list/play-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ListsComponent,
    ListComponent,
    PlayListComponent,
    TechdetailsComponent,
    CallbackComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }), // Alwats keep as first module in imports
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'callback', component: CallbackComponent },      
      { path: 'techdetails', component: TechdetailsComponent },
      { path: 'lists', component: ListsComponent },
      { path: 'lists/:id', component: ListComponent },
      { path: 'play/:id', component: PlayListComponent },
      { path: '**', redirectTo: '' }, // Unknown redirect to HomeComponent
    ]),
    BrowserAnimationsModule, // NoopAnimationsModule
    MaterialModule,
  ],
  providers: [ LogService, ListService, AuthService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
