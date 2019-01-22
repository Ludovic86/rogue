import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { JoueurVm } from '../models/joueurvm.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  isAuth: boolean;
  isReady: boolean = false;
  loggedJoueur: JoueurVm = {nom: "", email: ""};

  constructor(private authService: AuthService) {}

  ngOnInit() {
    debugger;
    this.init();
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
