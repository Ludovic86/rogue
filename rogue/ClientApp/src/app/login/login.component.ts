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

  Url: string = "";
  joueurForm: FormGroup;
  isAuth: boolean;
  isReady: boolean = false;
  responseString: string;
  joueurNotFound: boolean = false;
  loggedJoueur: JoueurVm;
  joueurSubscirption: Subscription;
  AuthSubsription: Subscription;

  constructor(private http: HttpClient, 
              private formBuilder: FormBuilder, 
              private route: ActivatedRoute, 
              private router: Router, @Inject('BASE_URL') baseUrl: string,
              private authService: AuthService,
              public snackBar: MatSnackBar) {
    this.Url = baseUrl;
    this.joueurForm = this.formBuilder.group({
      Email: ['', Validators.required],
      MotDePasse: ['', Validators.required]
    })
  }

  ngOnInit() {
    this.init();
    this.isReady = true;
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
  }

  // onSubmitForm() {
  //   const formValue = this.joueurForm.value;
  //   console.log(formValue);
  //   var joueur = new Joueur(
  //     formValue['Email'],
  //     formValue['MotDePasse']);
  //   this.http.post(this.Url + "api/Joueur/Authentification", joueur, {responseType: 'text'}).subscribe(result => {
  //     this.responseString = result;
  //     if (this.responseString == 'ok') {
  //       //this.authService.isAuth = true;
  //       alert("Compte authentifié avec succés")
  //       this.router.navigate(['/']);
  //     }
  //     this.joueurNotFound = true;
  //   }, error => console.log(error));
  //   return;
  // }

  onSubmitForm() {
    this.authService.authResponseSubject.subscribe(
      (authRes: string) =>{
        if (authRes == "notOk"){
          this.joueurNotFound = true;
          return
        }
      }
    );
    this.authService.authJoueur(this.joueurForm);
    // console.log(this.responseString);
    // if (this.responseString == "notOk"){
    //   debugger;
    //   this.joueurNotFound = true;
    //   debugger;
    //   return;
    // }
    this.snackBar.open(this.responseString, "", {
      duration: 2000,
    });
    //this.router.navigate(['/']);
  }

  unLog(){
    this.http.get(this.Url + "api/Joueur/Unlog").subscribe(result =>{
    }, error => console.log(error));
    this.isAuth = false;
    this.router.navigate(['/']);
  }

  

}
