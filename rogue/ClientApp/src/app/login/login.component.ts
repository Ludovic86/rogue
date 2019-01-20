import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Joueur } from '../Models/Joueur.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})



export class LoginComponent implements OnInit {

  Url: string = "";
  joueurForm: FormGroup;
  isAuth: boolean;
  responseString: string;
  joueurNotFound: boolean = false;
  public loggedJoueur: string;

  constructor(private http: HttpClient, private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, @Inject('BASE_URL') baseUrl: string) {
    this.Url = baseUrl;
    this.joueurForm = this.formBuilder.group({
      Email: ['', Validators.required],
      MotDePasse: ['', Validators.required]
    })
  }

  ngOnInit() {
    debugger;
    this.authCheck();
    if (this.loggedJoueur != undefined){
      this.isAuth = true;
    } else {
      this.isAuth = false;
    }
  }

  onSubmitForm() {
    const formValue = this.joueurForm.value;
    console.log(formValue);
    var joueur = new Joueur(
      formValue['Email'],
      formValue['MotDePasse']);
    this.http.post(this.Url + "api/Joueur/Authentification", joueur, {responseType: 'text'}).subscribe(result => {
      this.responseString = result;
      if (this.responseString == 'ok') {
        //this.authService.isAuth = true;
        alert("Compte authentifié avec succés")
        this.router.navigate(['/']);
      }
      this.joueurNotFound = true;
    }, error => console.log(error));
    return;
    
  }

  unLog(){
    this.http.get(this.Url + "api/Joueur/Unlog").subscribe(result =>{
    }, error => console.log(error));
    this.isAuth = false;
    this.router.navigate(['/']);
  }

  authCheck() {
    this.http.get(this.Url + "api/Joueur/AuthCheck", {responseType: 'text'}).subscribe(result => {
    console.log(result);
    this.loggedJoueur = result;
    console.log(this.loggedJoueur);
    }, error => console.log(error));
  }

}
