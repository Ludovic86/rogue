import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validator, Validators, FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { Joueur } from '../Models/Joueur.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ErrorStateMatcher } from '@angular/material';

export class RegistrationValidator {
  static validate(accountForm: FormGroup) {
      let password = accountForm.controls.MotDePasse.value;
      let repeatPassword = accountForm.controls.ConfirmPass.value;

      if (repeatPassword.length <= 0) {
          return null;
      }

      if (repeatPassword !== password) {
          return {
              doesMatchPassword: true
          };
      }

      return null;

  }
}

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css']
})
export class CreateAccountComponent implements OnInit {

  accountForm: FormGroup;
  Url: string = "";
  AjoutResult: string = "";
  
  constructor(private http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string, 
    private formBuilder: FormBuilder, 
    private router: Router) {
      this.Url = baseUrl;
      this.accountForm = this.formBuilder.group({
        IdJoueur: [0],
        NomJoueur: ['', Validators.required],
        Email: ['', [Validators.required, Validators.email]],
        MotDePasse: ['', [Validators.required, Validators.minLength(6)]],
        ConfirmPass: ['', [Validators.required]]
     }, {
       validator: RegistrationValidator.validate.bind(this)
     });
    }

  ngOnInit() {
  }

  checkPasswords(group: FormGroup) {
  let pass = group.controls.password.value;
  let confirmPass = group.controls.confirmPass.value;

  return pass === confirmPass ? null : { notSame: true }     
  }

  onSubmitForm(){
    const formValue = this.accountForm.value;
    const newPlayer = new Joueur(
      formValue["Email"],
      formValue["MotDePasse"],
      formValue["NomJoueur"]
    )
    debugger;
      this.http.post(this.Url + "api/Joueur/AjouterJoueur", newPlayer, {responseType: 'text'}).subscribe(
        result => {
          this.AjoutResult = result;
          console.log(this.AjoutResult);
          if (this.AjoutResult == "existe"){
            debugger;
            alert("Il existe déjà un compte enregistré avec cet E-mail")
          }
          else if (this.AjoutResult == "ok"){
            alert("Le compte a été enregistré avec succés")
            this.router.navigate(['/'])
          }
        }, error => console.log(error)
      );
  }

}
