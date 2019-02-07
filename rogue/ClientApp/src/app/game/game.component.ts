import { Component, OnInit, ViewChild, TemplateRef, OnDestroy } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { JoueurVm } from '../models/joueurvm.model';
import { GameService } from '../services/game.service';
import { PartieVM, Personnage, Donjon, Game, Salle, Ennemi, Item } from '../models/game.model';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit, OnDestroy {

  modalRef: BsModalRef;
  isAuth: boolean;
  isReady: boolean;
  loggedJoueur: JoueurVm;
  partieEnCours: PartieVM;
  isCollapsed = true;
  selectionPerso: Personnage[];
  selectionDonjon: Donjon[];
  selectedPerso: Personnage;
  selectedDonjon: Donjon;
  newGame: Game;
  currentRoom: Salle;
  currentEnnemi: Ennemi;
  currentItem: Item;
  currentEnnemiHP: number;
  atkReady: boolean = true;
  atkCountDown: number;
  ennemiCountDown: number;
  lootOpened: boolean;
  savedGameLoaded: boolean;

  @ViewChild('Loot') templateLoot: TemplateRef<any>;
  @ViewChild('noLoot') templateNoLoot: TemplateRef<any>;

  constructor(private authService: AuthService, private gameService: GameService, private modalService: BsModalService) { 
    
  }

  ngOnInit() {
    this.init();
  }

  ngOnDestroy(){
    this.currentEnnemi = undefined;
    this.newGame = undefined;
    this.currentItem = undefined;
    this.currentRoom = undefined;
    this.selectedPerso = undefined;
    this.selectedDonjon = undefined;
  }

  init(){
    this.authService.authSubject.subscribe(
      (auth: boolean) =>{
        this.isAuth = auth;
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
    this.gameService.partieEnCoursSub.subscribe(
      (partie: PartieVM) =>{
        this.partieEnCours = partie;
        if (this.partieEnCours == null){
          debugger;
          this.creeNouvellePartie();
        }
        this.isReady = true;
      }
    );
    this.gameService.getPartieEnCours();
  }

  imgStringConstructor(img: string) : string{
    return "../../assets/img/" + img + ".png";
  }

  potraitStringConstructor(img : string) : string{
    return "../../assets/img/" + img + "Portrait.png";
  }

  creeNouvellePartie(){
    this.currentEnnemi = null;
    this.currentItem = null;
    this.partieEnCours = null;
    this.isReady = false;
    this.terminePartie(this.loggedJoueur);
    this.gameService.selectionPersoSub.subscribe(
      (persos: Personnage[]) =>{
        this.selectionPerso = persos;
      }
    );
    this.gameService.emitSelectionPerso();
    this.gameService.getSelectionPerso();
    this.gameService.selectionDonjonSub.subscribe(
      (donjons: Donjon[]) =>{
        this.selectionDonjon = donjons;
      }
    );
    this.gameService.emitSelectionDonjon();
    this.gameService.getSelectionDonjon();
    this.gameService.newGameSub.subscribe(
      (game: Game) =>{
        this.newGame = game;
      }
    );
    this.gameService.emitNewGame();
    this.gameService.getNewGame();
    this.isReady = true;
  }

  async getPartieSauvegardee(){
    debugger;
    this.currentEnnemi = null;
    this.currentItem = null;
    this.partieEnCours = null;
    this.isReady = false;
    this.gameService.newGameSub.subscribe(
      (game: Game) =>{
        debugger;
        this.newGame = game;
      }
    );
    this.gameService.getSavedGame();
    this.gameService.emitNewGame();
    await this.delay(4000);
    this.selectedPerso = this.newGame.personnage;
    this.selectedDonjon = this.newGame.donjon;
    this.calculateBonus();
    this.generateRoom(0);
    this.isReady = true;
  }

  async delay(ms: number){
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  terminePartie(joueur: JoueurVm){
    this.gameService.terminePartie(joueur);
    this.partieEnCours = null;
  }

  setSelectedPerso(perso: Personnage){
    this.selectedPerso = perso;
    this.newGame.personnage = this.selectedPerso;
    this.selectionPerso = null;
  }

  setSelectedDonjon(donjon: Donjon){
    this.selectedDonjon = donjon;
    this.newGame.donjon = this.selectedDonjon;
    this.selectionDonjon = null;
    this.newGame.hpLeft = this.newGame.personnage.hpPerso;
    this.generateRoom(0);
  }

  generateRoom(state: number){
    this.isReady = false;
    switch (state){
      case 0 : {
        this.genererSalle();
        this.genererEnnemi();
        this.genererItem();
        this.isReady = true;
        break;
      }
      case 1 : {
        this.removeSalle(this.currentRoom);
        this.newGame.sallesParcourues = this.newGame.sallesParcourues || [];
        this.newGame.sallesParcourues.push(this.currentRoom);
        this.newGame.nbreSalle = this.newGame.sallesParcourues.length;
        this.gameService.saveGame(this.newGame);
        this.atkReady = false;   
        this.lootOpened = false;
        this.currentRoom = null;
        this.currentItem = null;
        this.currentEnnemi = null;
        this.genererSalle();
        this.genererEnnemi();
        this.genererItem();
        this.isReady = true;
        break;
      }
      case 2 : {
        this.genererSalle();
        this.genererEnnemi();
        this.genererItem();
        this.newGame.nbreSalle = this.newGame.sallesParcourues.length;
        break;
      }
    } 
  }

  removeElementFromGame(element: number, list: any[]) : any[]{
    var index = list.indexOf(list[element]);
    if (index > -1){
      list.splice(index, 1);
    }
    return list;
  }

  removeSalle (salle: Salle) : void {
    for (let i = 0; i < this.newGame.salles.length; i++){
      if (salle == this.newGame.salles[i]){
        this.newGame.salles.splice(i, 1);
      }
    }
  }

  genererSalle() : void {
    let i = Math.floor(Math.random() * this.newGame.salles.length) + 0;
    this.currentRoom = this.newGame.salles[i];
  }

  genererEnnemi() : void {
    if (this.rollFifty()){
      let i = Math.floor(Math.random() * this.newGame.ennemis.length) + 0;
      this.currentEnnemi = this.newGame.ennemis[i];
      this.currentEnnemiHP = this.currentEnnemi.pvEnnemi;
      this.timerPerso();
      this.timerEnnemi();
    }
  }

  genererItem() : void {
    if (this.rollFifty()){
      let i = Math.floor(Math.random() * this.newGame.objets.length) + 0;
      this.currentItem = this.newGame.objets[i];
      this.newGame.objets = this.removeElementFromGame(i, this.newGame.objets);
    }
  }

  rollFifty() : boolean{
    var i = Math.round(Math.random());
    if (i == 0){
      return true;
    }
    return false;
  }

  attaquer(){
    this.currentEnnemiHP = this.currentEnnemiHP - this.newGame.personnage.atkPerso;
    this.atkReady = true;
    this.timerPerso();
    if (this.currentEnnemiHP < 0){
      this.currentEnnemiHP = 0;
    }
  }

  attaquerEnnemi(){
    debugger;
    if (this.currentEnnemi == undefined){
      return;
    }
    if (this.currentEnnemiHP > 0){
      this.newGame.hpLeft = this.newGame.hpLeft - this.currentEnnemi.atkEnnemi;
      this.timerEnnemi();
      if (this.newGame.hpLeft < 0){
        this.newGame.hpLeft = 0;
      }
    } 
  }

  timerEnnemi(){
    if (this.currentEnnemiHP == 0 || this.currentEnnemi == undefined){
      return;
    }
    this.ennemiCountDown = 0;
    var interval = setInterval(() =>{
      this.ennemiCountDown++
      if (this.ennemiCountDown == this.currentEnnemi.speedEnnemi){
        this.attaquerEnnemi()
        clearInterval(interval);
      };
    }, 1000)
  }

  timerPerso(){
    if (this.currentEnnemiHP == 0){
      this.atkReady = true;
      return;
    }
    this.atkCountDown = 0;
    var interval = setInterval(() =>{
      this.atkCountDown++
      if (this.atkCountDown == this.newGame.personnage.speedPerso){
          this.atkReady = false;
          clearInterval(interval);
      };
    }, 1000)
  }

  openLoot(){
    this.lootOpened = true;
    if(this.currentItem != undefined){
      this.openModal(this.templateLoot)
      return;
    }
    this.openModal(this.templateNoLoot);
  }
  openModal(template: TemplateRef<any>){
    this.modalRef = this.modalService.show(template)
  }

  confirmItem() : void{
    this.newGame.inventaire = this.newGame.inventaire || [];
    if (this.currentItem.atkItem > 0){
      this.newGame.personnage.atkPerso = this.newGame.personnage.atkPerso + this.currentItem.atkItem;
    }
    if (this.currentItem.speedItem > 0){
      this.newGame.personnage.speedPerso = this.newGame.personnage.speedPerso - this.currentItem.speedItem;
      if (this.newGame.personnage.speedPerso < 1){
        this.newGame.personnage.speedPerso = 1;
      }
    }
    this.newGame.inventaire.push(this.currentItem);
    this.modalRef.hide();
  }

  calculateBonus(){
    for (let item of this.newGame.inventaire){
      if (item.atkItem > 0){
        this.newGame.personnage.atkPerso = this.newGame.personnage.atkPerso + item.atkItem;
      }
      if (item.speedItem > 0){
        this.newGame.personnage.speedPerso = this.newGame.personnage.speedPerso - item.speedItem;
        if (this.newGame.personnage.speedPerso < 1){
          this.newGame.personnage.speedPerso = 1;
        }
      }
    }
  }

  declineItem() : void{
    this.modalRef.hide();
  }


}
