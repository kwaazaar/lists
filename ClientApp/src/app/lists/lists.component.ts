import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ListService } from '../services/list.service';

import { ListSummary } from '../models/list-summary';
import { ListModel } from '../models/list-model';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {

  lists: ListSummary[];

  constructor(private router: Router, private listService: ListService) { }

  ngOnInit() {
    this.getListSummaries();
  }

  getListSummaries(): void {
    this.listService.getListSummaries()
      .subscribe(items => this.lists = items);
  }

  add(name: string): void {
    name = name.trim();
    if (!name) { return; }

    const list = new ListModel();
    list.name = name;

    this.listService.addList(list)
      .subscribe(result => this.getListSummaries());
  }

  delete(list: ListSummary): void {
    this.listService.deleteList(list)
      .subscribe(deleted => this.getListSummaries()); // don't care about result
  }

  play(list: ListSummary): void {
    console.log('about to play list:', list);
    console.log('route: ', `/play/${list.id}`);
    this.router.navigate([`/play/${list.id}`]);
  }
}
