import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { JoueurVm } from '../models/joueurvm.model';
import { GameService } from '../services/game.service';
import { PartieVM, Personnage, Donjon, Game, Salle, Ennemi, Item } from '../models/game.model';
import { CollapseModule } from 'ngx-bootstrap';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

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

  constructor(private authService: AuthService, private gameService: GameService) { }

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
    var str = "../../assets/img/" + img + ".png";
    return str;
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
    this.generateRoom();
  }

  generateRoom(){
    this.rollFight()
    var i = Math.floor(Math.random() * this.newGame.salles.length) + 0;
    this.currentRoom = this.newGame.salles[i];
    this.newGame.salles = this.removeElementFromGame(i, this.newGame.salles);
    if (this.rollFight()){
      i = Math.floor(Math.random() * this.newGame.ennemis.length) + 0;
      this.currentEnnemi = this.newGame.ennemis[i];
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
    debugger;
    var i = Math.round(Math.random());
    if (i == 0){
      return true;
    }
    return false;
  }


}
