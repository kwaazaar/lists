import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ListService } from './services/list.service';
import { AuthService } from './services/auth.service';
import { AuthGuardService as AuthGuard, AuthGuardService } from './services/auth-guard.service';

import { HomeComponent } from './home/home.component';
import { TechdetailsComponent } from './techdetails/techdetails.component';
import { MaterialModule } from './modules/material.module';
import { ListsComponent } from './lists/lists.component';
import { LogService } from './services/log.service';
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
    TechdetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }), // Alwats keep as first module in imports
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'techdetails', component: TechdetailsComponent },
      { path: 'lists', component: ListsComponent, canActivate: [AuthGuard] },
      { path: 'lists/:id', component: ListComponent, canActivate: [AuthGuard]},
      { path: 'play/:id', component: PlayListComponent, canActivate: [AuthGuard] },
    ]),
    BrowserAnimationsModule, // NoopAnimationsModule
    MaterialModule,
  ],
  providers: [ LogService, ListService, AuthService, AuthGuardService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
