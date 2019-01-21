import { HttpClient } from "@angular/common/http";
import { Inject, Injectable, OnInit } from "@angular/core";
import { JoueurVm } from "../models/joueurvm.model";
import { ActivatedRoute } from "@angular/router";
import { Subject, Observable, observable } from "rxjs";
import { FormGroup } from "@angular/forms";
import { Joueur } from "../models/joueur.model";

@Injectable()
export class AuthService{
    private isAuth: boolean;
    Url: string;
    authSubject = new Subject<boolean>();
    authResponseSubject = new Subject<string>();
    authResponse : string;
    public observable: Observable<any>;
    loggedPlayer: JoueurVm;
    joueurSubject = new Subject<JoueurVm>();

    constructor (private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute){
        this.Url = baseUrl;
    }

    emitJoueurSubject(){
        this.joueurSubject.next(this.loggedPlayer);
    }

    emitAuthSubject(){
        this.authSubject.next(this.isAuth);
    }

    emitAuthResponseSubject(){
        this.authResponseSubject.next(this.authResponse)
    }

    loggedJoueur(){
        this.http.get<JoueurVm>(this.Url + "api/Joueur/LoggedJoueur")
        .subscribe((response) =>{
            this.loggedPlayer = response;
            this.emitJoueurSubject();
        },
        (error) =>{
            console.log(error);
        })
    }

    authCheck(){
        this.http.get<boolean>(this.Url + "api/Joueur/AuthCheck")
        .subscribe((response) =>{
            this.isAuth = response;
            this.emitAuthSubject();
        },
        (error) => {
            console.log(error)
        }
        );
    }

    authJoueur(joueurForm: FormGroup) {
        debugger;
        const formValue = joueurForm.value;
        var joueur = new Joueur(
          formValue['Email'],
          formValue['MotDePasse']);
        this.http.post(this.Url + "api/Joueur/Authentification", joueur, {responseType: 'text'}).subscribe(result => {
          this.authResponse = result;
            this.emitAuthResponseSubject();
        }, error => console.log(error));
      }

}