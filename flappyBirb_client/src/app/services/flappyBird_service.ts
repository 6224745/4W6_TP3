import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Score } from '../models/score';

const domain = "https://localhost:7038/";

@Injectable({
  providedIn: 'root'
})
export class FlappyBirdService {

  constructor(private http: HttpClient) { }

  async register(user : string, mail : string, pass : string, passCon : string) : Promise<void> {

    let registerDTO = {
        username : user, 
        email : mail, 
        password : pass, 
        passwordConfirm : passCon
    };

    let x = await lastValueFrom(this.http.post<any>(domain + "api/Users/Register", registerDTO));
    console.log(x);  
  }

  async login(user : string, pass : string) : Promise<void>{

    let loginDTO =  {
        username : user,
        password : pass
    };

    let x = await lastValueFrom(this.http.post<any>(domain + "api/Users/Login", loginDTO));
    console.log(x);

    // 🔑 Très important de stocker le token quelque part pour pouvoir l'utiliser dans les futures requêtes !
    localStorage.setItem("token", x.token);
  }

  async postScore(score : Score) : Promise<Score>{
    let x = await lastValueFrom(this.http.post<Score>(domain + "api/Scores/PostScore", score));
    console.log(x);
    return x;
  }
}
