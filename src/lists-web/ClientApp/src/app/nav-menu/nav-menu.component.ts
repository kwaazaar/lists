import { Component, Input, EventEmitter, Output } from '@angular/core';
import { NavItem } from './nav-item';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  @Output() navigated: EventEmitter<NavItem> = new EventEmitter();

  navItems: NavItem[] = [
    // text, routerLink, icon
    new NavItem('Home', '/', 'home'),
    new NavItem('Technical Details', '/techdetails', 'category'), // category build code
    new NavItem('My word lists', '/lists', 'list'),
  ];

  constructor() {
  }

  toggle(navItem: NavItem): void {
    console.log('menu toggle!');
    this.navigated.emit(navItem);
  }
}
