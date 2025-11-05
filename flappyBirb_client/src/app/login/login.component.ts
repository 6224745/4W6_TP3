import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialModule } from '../material.module';
import { FormsModule } from '@angular/forms';
import { FlappyBirdService } from '../services/flappyBird_service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MaterialModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  hide = true;

  registerUsername : string = "";
  registerEmail : string = "";
  registerPassword : string = "";
  registerPasswordConfirm : string = "";

  loginUsername : string = "";
  loginPassword : string = "";

  constructor(public route : Router, public flappyService : FlappyBirdService) { }

  ngOnInit() {
  }

  login(){
    this.flappyService.login(this.loginUsername, this.loginPassword);
    // Redirection si la connexion a réussi :
    this.route.navigate(["/play"]);
  }

  register(){
    this.flappyService.register(this.registerUsername, this.registerEmail, this.registerPassword, this.registerPasswordConfirm);
  }
  
}
