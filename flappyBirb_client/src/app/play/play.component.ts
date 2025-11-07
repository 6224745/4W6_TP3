import { Component, OnInit } from '@angular/core';
import { Game } from './gameLogic/game';
import { MaterialModule } from '../material.module';
import { CommonModule } from '@angular/common';
import { FlappyBirdService } from '../services/flappyBird_service';
import { Score } from '../models/score';

@Component({
  selector: 'app-play',
  standalone: true,
  imports: [MaterialModule, CommonModule],
  templateUrl: './play.component.html',
  styleUrl: './play.component.css'
})
export class PlayComponent implements OnInit{

  game : Game | null = null;
  scoreSent : boolean = false;

  constructor(public flappyService : FlappyBirdService){}

  ngOnDestroy(): void {
    // Ceci est crotté mais ne le retirez pas sinon le jeu bug.
    location.reload();
  }

  ngOnInit() {
    this.game = new Game();
  }

  replay(){
    if(this.game == null) return;
    this.game.prepareGame();
    this.scoreSent = false;
  }

  sendScore(){
    if(this.scoreSent) return;

    this.scoreSent = true;
    
    // ██ Appeler une requête pour envoyer le score du joueur ██
    // Le score est dans sessionStorage.getItem("score")
    // Le temps est dans sessionStorage.getItem("time")
    // La date sera choisie par le serveur

    let scoreJSON = sessionStorage.getItem("score");
    let tempsJSON = sessionStorage.getItem("time");

    if(scoreJSON != null && tempsJSON != null){
      let convertedScore = JSON.parse(scoreJSON);
      let convertedTemps = JSON.parse(tempsJSON);

      let newScore = new Score(0, null, null, convertedTemps, convertedScore, false)
      this.flappyService.postScore(newScore);
    }

    this.scoreSent = false;
  }


}
