import { Component, Input, EventEmitter, Output } from '@angular/core';
import { NavItem } from './nav-item';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  @Output() navigated: EventEmitter<NavItem> = new EventEmitter();

  navItems: NavItem[] = [];

  constructor(private auth: AuthService) {
    this.navItems = [
      // text, routerLink, icon
      new NavItem('Home', '/', 'home'),
      new NavItem('Technical Details', '/techdetails', 'category'), // category build code
    ];

    if (auth.isAuthenticated()) {
      this.navItems.push(new NavItem('My word lists', '/lists', 'list'));
    }
  }

  toggle(navItem: NavItem): void {
    console.log('menu toggle!');
    this.navigated.emit(navItem);
  }
}
