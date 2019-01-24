import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { JoueurVm } from '../models/joueurvm.model';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  isAuth: boolean;
  isReady: boolean;
  loggedJoueur: JoueurVm;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.init();
    this.isReady = true;
  }

  init(){
    this.authService.authSubject.subscribe(
      (auth: boolean) =>{
        this.isAuth = auth;
        if (this.isAuth != undefined){
          this.isReady = true;
        }
      }
    );
    this.authService.authCheck();
    this.authService.emitAuthSubject();
    this.authService.joueurSubject.subscribe(
      (joueur: JoueurVm) =>{
        this.loggedJoueur = joueur;
      }
    );
    this.authService.emitJoueurSubject();
    this.authService.loggedJoueur();
  }

}
