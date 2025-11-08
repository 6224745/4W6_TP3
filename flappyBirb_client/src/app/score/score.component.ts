import { Component } from '@angular/core';
import { Score } from '../models/score';
import { MaterialModule } from '../material.module';
import { CommonModule } from '@angular/common';
import { Round00Pipe } from '../pipes/round-00.pipe';
import { FlappyBirdService } from '../services/flappyBird_service';

@Component({
  selector: 'app-score',
  standalone: true,
  imports: [MaterialModule, CommonModule, Round00Pipe],
  templateUrl: './score.component.html',
  styleUrl: './score.component.css'
})
export class ScoreComponent {

  myScores : Score[] = [];
  publicScores : Score[] = [];

  constructor(public flappyService : FlappyBirdService) { }

  async ngOnInit() {
    this.publicScores = await this.flappyService.getPublicScores();
    this.myScores = await this.flappyService.getMyScores()
  }

  async changeScoreVisibility(score : Score){


  }

}
