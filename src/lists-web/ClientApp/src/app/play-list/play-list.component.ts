import { Component, OnInit } from '@angular/core';
import { ListService } from '../services/list.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ListModel } from '../models/list-model';
import { ListItem } from '../models/list-item';

@Component({
  selector: 'app-play-list',
  templateUrl: './play-list.component.html',
  styleUrls: ['./play-list.component.css']
})
export class PlayListComponent implements OnInit {

  list: ListModel;
  currentQuestion: ListItem;

  // Play parameters
  isRunning = false;
  questionTime = 5000; // For testing purposes
  nrCorrect = 0;
  nrPlayed = 0;
  score = 0;
  questionsDone: number[];

  intervalId: number;

  constructor(private router: Router, private route: ActivatedRoute, private listService: ListService) { }

  ngOnInit() {
    this.getListDetails();
  }

  getListDetails(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.listService.getList(id)
      .subscribe(item => this.list = item);
  }

  private resetStats(): void {
    this.currentQuestion = null;
    this.nrCorrect = 0;
    this.nrPlayed = 0;
    this.score = 0;
    this.questionsDone = [];
  }

  private pickNextQuestion(): ListItem {
    const questionCount = this.list.items.length;

    // Check if all questions have been challenged
    if (questionCount <= this.questionsDone.length) {
      return null;
    }

    // Try to find index for next question
    let qId = -1;
    do {
      qId = Math.floor(Math.random() * questionCount);
      // console.log('Random index: ', qId, this.questionsDone.indexOf(qId));
    } while (this.questionsDone.indexOf(qId) >= 0); // Retry when the question was already done

    // Get the listitem for the new question
    const listItem = this.list.items[qId];
    this.questionsDone.push(qId); // add it to the 'done'-list
    return listItem;
  }

  validateAnswer(answer: string) {
    if (this.intervalId) {
      window.clearInterval(this.intervalId);
      this.intervalId = -1;
    }

    if (this.currentQuestion) {
      const correct = (answer.trim().toUpperCase() === this.currentQuestion.answer.toUpperCase());
      this.currentQuestion = null;
      this.nrPlayed++;
      if (correct) { this.nrCorrect++; }
      this.score = (this.nrCorrect / this.nrPlayed) * 100.0;
    }
    this.nextQuestion();
    this.intervalId = window.setInterval(() => this.nextQuestion(), this.questionTime);
  }

  nextQuestion(): void {
    // Update stats if still current answer
    if (this.currentQuestion) {
      this.nrPlayed++;
      this.score = (this.nrCorrect / this.nrPlayed) * 100.0;
    }

    // Serve next question
    this.currentQuestion = this.pickNextQuestion();
    if (this.currentQuestion) {
      console.log('nextQuestion:', this.currentQuestion);
    } else {
      console.log('no more questions');
      this.stop();
    }
  }

  start(): void {
    this.resetStats();
    this.isRunning = true;
    this.nextQuestion();
    this.intervalId = window.setInterval(() => this.nextQuestion(), this.questionTime);
  }

  stop(): void {
    this.isRunning = false;
    if (this.intervalId) {
      window.clearInterval(this.intervalId);
    }
  }
}
