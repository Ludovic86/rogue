import { Component, OnInit, TemplateRef, OnDestroy } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { JoueurVm } from '../models/joueurvm.model';
import { GameService } from '../services/game.service';
import { PartieVM } from '../models/game.model';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, OnDestroy {

  isAuth: boolean;
  isReady: boolean = false;
  loggedJoueur: JoueurVm = {nom: "", email: ""};
  historiqueParties: PartieVM[];
  modalRef: BsModalRef;

  constructor(private authService: AuthService, private gameService: GameService, private modalService: BsModalService) {}

  ngOnInit() {
    this.init();
  }

  ngOnDestroy(){
    this.historiqueParties = undefined;
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
        if (this.loggedJoueur != undefined){
          this.gameService.partieHistoSub.subscribe(
            (historique: PartieVM[]) =>{
              this.historiqueParties = historique;
            }
          );
        }
      }
    );  
    this.authService.emitJoueurSubject();
    this.authService.loggedJoueur();
    this.gameService.emitPartieHisto();
    this.gameService.getHistoriqueParties();
  }

  async delay(ms: number){
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  openModal(template: TemplateRef<any>){
    this.modalRef = this.modalService.show(template)
  }
}
