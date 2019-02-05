import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute } from "@angular/router";
import { PartieVM, Personnage, Donjon, Game } from "../models/game.model";
import { Subject } from "rxjs";
import { JoueurVm } from "../models/joueurvm.model";

@Injectable()
export class GameService{

    Url: string;
    partieEnCours: PartieVM;
    partieEnCoursSub = new Subject<PartieVM>();
    selectionPerso: Personnage[];
    selectionPersoSub = new Subject<Personnage[]>();
    selectionDonjon: Donjon[];
    selectionDonjonSub = new Subject<Donjon[]>();
    newGame: Game;
    newGameSub = new Subject<Game>();
    partieHisto: PartieVM[];
    partieHistoSub = new Subject<PartieVM[]>();

    constructor (private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute){
        this.Url = baseUrl;
    }

    emitPartieEnCours(){
        this.partieEnCoursSub.next(this.partieEnCours);
    }

    emitSelectionPerso(){
        this.selectionPersoSub.next(this.selectionPerso);
    }

    emitSelectionDonjon(){
        this.selectionDonjonSub.next(this.selectionDonjon);
    }

    emitNewGame(){
        this.newGameSub.next(this.newGame);
    }

    emitPartieHisto(){
        this.partieHistoSub.next(this.partieHisto);
    }

    getPartieEnCours(){
        this.http.get<PartieVM>(this.Url + 'api/Game/GetCurrentPartie/')
        .subscribe((response) =>{
            this.partieEnCours = response;
            this.emitPartieEnCours();
        },
        (error) =>{
            console.log(error);
        })
    }

    terminePartie(joueur: JoueurVm){
        this.http.post<JoueurVm>(this.Url + 'api/Game/TerminerPartie', joueur).subscribe(result =>{
        }, error => console.log(error));
    }

    getSelectionPerso(){
        this.http.get<Personnage[]>(this.Url + 'api/Game/GetPersonnages')
        .subscribe((response) =>{
            this.selectionPerso = response;
            this.emitSelectionPerso();
        },
        (error) =>{
            console.log(error);
        })
    }

    getSelectionDonjon(){
        this.http.get<Donjon[]>(this.Url + 'api/Game/GetDonjons')
        .subscribe((response) =>{
        this.selectionDonjon = response;
        this.emitSelectionDonjon();
        },
        (error) =>{
            console.log(error);
        })
    }

    getNewGame(){
        this.http.get<Game>(this.Url + 'api/Game/CreateNewGame')
        .subscribe((response) =>{
            console.log('response' + response);
            this.newGame = response;
            this.emitNewGame();
        },
        (error) =>{
            console.log(error);
        })
    }

    getSavedGame(){
        this.http.get<Game>(this.Url + 'api/Game/GetSavedGame')
        .subscribe((response) =>{
            console.log(response);
            this.newGame = response;
            this.emitNewGame();
        },
        (error) =>{
            console.log(error);
        })
    }

    getHistoriqueParties(){
        this.http.get<PartieVM[]>(this.Url + 'api/Game/HistoriqueParties')
        .subscribe((response) =>{
            this.partieHisto = response;
            this.emitPartieHisto();
        },
        (error) =>{
            console.log(error);
        })
    }

    saveGame(game: Game){
        this.http.post<Game>(this.Url + 'api/Game/SaveGame', game).subscribe(result =>{
        }, error => console.log(error));
    }
    

}