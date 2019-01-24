import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Joueur } from '../models/joueur.model';
import { AuthService } from '../services/auth.service';
import { JoueurVm } from '../models/joueurvm.model';
import { Subscription } from 'rxjs';
import { MatDialog, MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  joueurForm: FormGroup;
  isAuth: boolean;
  isReady: boolean = false;
  responseString: string;
  joueurNotFound: boolean = false;
  loggedJoueur: JoueurVm = {nom: "", email: ""};
  joueurSubscirption: Subscription;
  AuthSubsription: Subscription;

  constructor(private formBuilder: FormBuilder, 
              private router: Router, @Inject('BASE_URL') baseUrl: string,
              private authService: AuthService,
              public snackBar: MatSnackBar) {
    this.joueurForm = this.formBuilder.group({
      Email: ['', Validators.required],
      MotDePasse: ['', Validators.required]
    })
  }

  ngOnInit() {
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

  onSubmitForm() {
    this.isReady = false;
    this.authService.authResponseSubject.subscribe(
      (authRes: string) =>{
        if (authRes == "notOk"){
          this.isReady = true;
          this.joueurNotFound = true;
          return
        } 
        else{
          this.snackBar.open("Compte authentifié avec succés", "Info", {
            duration: 2000,
            verticalPosition: 'top',
            horizontalPosition: 'center',
          });
          this.router.navigate(['/']);
        }
      }
    );
    this.authService.authJoueur(this.joueurForm);
  }

  unLog(){
    this.authService.unLog();
    this.isAuth = false;
    this.router.navigate(['/']);
  }

  

}
