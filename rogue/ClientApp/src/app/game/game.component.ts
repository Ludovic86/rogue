import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { JoueurVm } from '../models/joueurvm.model';
import { GameService } from '../services/game.service';
import { PartieVM, Personnage, Donjon, Game, Salle, Ennemi, Item } from '../models/game.model';
import { CollapseModule, BsModalService, BsModalRef } from 'ngx-bootstrap';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

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
  currentPersoHP: number;
  atkReady: boolean = true;
  atkCountDown: number;
  ennemiCountDown: number;
  lootOpened: boolean;

  @ViewChild('Loot') templateLoot: TemplateRef<any>;
  @ViewChild('noLoot') templateNoLoot: TemplateRef<any>;

  constructor(private authService: AuthService, private gameService: GameService, private modalService: BsModalService) { }

  ngOnInit() {

    this.init();
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
          this.nouvellePartie();
        }
        this.isReady = true;
      }
    );
    this.gameService.getPartieEnCours(this.loggedJoueur);
  }

  imgStringConstructor(img: string) : string{
    return "../../assets/img/" + img + ".png";
  }

  potraitStringConstructor(img : string) : string{
    return "../../assets/img/" + img + "Portrait.png";
  }

  nouvellePartie(){
    this.isReady = false;
    this.terminePartie(this.loggedJoueur);
    this.partieEnCours = null;
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
        console.log(this.newGame);
      }
    );
    this.gameService.emitNewGame();
    this.gameService.getGame();
    this.isReady = true;
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
    this.currentPersoHP = this.newGame.personnage.hpPerso;
    this.generateRoom();
  }

  generateRoom(){
    this.lootOpened = false;
    this.currentEnnemi = null;
    this.currentRoom = null;
    this.currentItem = null;
    var i = Math.floor(Math.random() * this.newGame.salles.length) + 0;
    this.currentRoom = this.newGame.salles[i];
    this.newGame.salles = this.removeElementFromGame(i, this.newGame.salles);
    if (this.rollFight()){
      i = Math.floor(Math.random() * this.newGame.ennemis.length) + 0;
      this.currentEnnemi = this.newGame.ennemis[i];
      this.currentEnnemiHP = this.currentEnnemi.pvEnnemi;
      this.timerPerso();
      this.timerEnnemi();
    }
    if (this.rollFight()){
      i = Math.floor(Math.random() * this.newGame.objets.length) + 0;
      this.currentItem = this.newGame.objets[i];
      this.newGame.objets = this.removeElementFromGame(i, this.newGame.objets);
    }
  }

  removeElementFromGame(element: number, list: any[]) : any[]{
    var index = list.indexOf(list[element]);
    if (index > -1){
      list.splice(index, 1);
    }
    return list;
  }

  rollFight() : boolean{
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
    if (this.currentEnnemiHP > 0){
      this.currentPersoHP = this.currentPersoHP - this.currentEnnemi.atkEnnemi;
      this.timerEnnemi();
      if (this.currentPersoHP < 0){
        this.currentPersoHP = 0;
      }
    } 
  }

  timerEnnemi(){
    if (this.currentEnnemiHP == 0){
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
    this.newGame.inventaire.push(this.currentItem);
    this.calculateBonus();
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
