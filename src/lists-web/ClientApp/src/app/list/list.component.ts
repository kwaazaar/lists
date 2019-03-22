import { Component, OnInit, ViewChild } from '@angular/core';
import { ListService } from '../services/list.service';
import { ListModel } from '../models/list-model';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ListItem } from '../models/list-item';
import { MatSort, MatTableDataSource, MatPaginator } from '@angular/material';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  list: ListModel;
  displayedColumns: string[] = ['question', 'answer', 'actions'];
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild('questionList') questionList;
  dataSource: MatTableDataSource<ListItem>;

  constructor(private listService: ListService, private route: ActivatedRoute, private location: Location) { }

  ngOnInit() {
    this.getListDetails();
  }

  getListDetails(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.listService.getList(id)
      .subscribe(list => {
        this.list = list;
        this.updateDataSource(this.list.items);
     });
  }

  private updateDataSource(items: ListItem[]): void {
    this.dataSource = new MatTableDataSource(items);
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
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
      .subscribe(item => {
        this.list.items.push(item);
        this.questionList.renderRows(); // Force rerender of rows
      });
  }

  deleteQuestion(listItem: ListItem): void {
    this.listService.deleteListItem(listItem)
      .subscribe(success => this.list.items = this.list.items.filter(item => item !== listItem));
  }
}
