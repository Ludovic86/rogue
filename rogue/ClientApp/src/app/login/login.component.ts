import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
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

  constructor(private http: HttpClient, private authService: AuthService,
    private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, @Inject('BASE_URL') baseUrl: string) {
      debugger;
    this.isAuth = this.authService.isAuth;
    this.Url = baseUrl;
    this.joueurForm = this.formBuilder.group({
      Email: ['', Validators.required],
      MotDePasse: ['', Validators.required]
    })
  }

  ngOnInit() {
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
        this.authService.isAuth = true;
        alert("Compte authentifié avec succés")
        this.router.navigate(['/']);
      }
      this.joueurNotFound = true;
    }, error => console.log(error));
    return;
    
  }

}
