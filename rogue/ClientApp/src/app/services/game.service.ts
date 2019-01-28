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

    getPartieEnCours(joueur: JoueurVm){
        this.http.post<PartieVM>(this.Url + 'api/Game/GetCurrentPartie/', joueur)
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

    getGame(){
        this.http.get<Game>(this.Url + 'api/Game/CreateNewGame')
        .subscribe((response) =>{
            this.newGame = response;
            this.emitNewGame();
        },
        (error) =>{
            console.log(error);
        })
    }
    

}