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
    new NavItem('Counter', '/counter', 'note_add'),
    new NavItem('Weather', '/fetch-data', 'cloud_queue'),
    new NavItem('Lists', '/lists', 'list'),
  ];

  constructor() {
  }

  toggle(navItem: NavItem): void {
    console.log('menu toggle!');
    this.navigated.emit(navItem);
  }
}
