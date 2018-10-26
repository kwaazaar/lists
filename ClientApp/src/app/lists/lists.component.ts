import { Component, OnInit } from '@angular/core';

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

  constructor(private listService: ListService) { }

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

  delete(id: number): void {
    // TODO
    // this.listService.deleteList(id)
    //   .subscribe(result => this.getListSummaries());
  }
}
