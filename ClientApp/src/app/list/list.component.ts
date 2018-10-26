import { Component, OnInit } from '@angular/core';
import { ListService } from '../services/list.service';
import { ListModel } from '../models/list-model';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ListItem } from '../models/list-item';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  list: ListModel;

  constructor(private listService: ListService, private route: ActivatedRoute, private location: Location) { }

  ngOnInit() {
    this.getListDetails();
  }

  getListDetails(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.listService.getList(id)
      .subscribe(item => this.list = item);
  }

  addQuestion(question: string, answer: string) {
    question = question.trim();
    if (!question) { return; }
    answer = answer.trim();
    if (!answer) { return; }

    const listItem = new ListItem();
    listItem.listId = this.list.id;
    listItem.question = question;
    listItem.answer = answer;

    this.listService.upsertListItem(listItem)
      .subscribe(item => this.list.items.push(item));
  }
}
