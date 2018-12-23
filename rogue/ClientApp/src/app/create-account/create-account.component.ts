import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validator, Validators, FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { Joueur } from '../Models/Joueur.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ErrorStateMatcher } from '@angular/material';

export class RegistrationValidator {
  static validate(accountForm: FormGroup) {
      let password = accountForm.controls.Password.value;
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
  newPlayer: Joueur;
  Url: string = "";
  
  constructor(private http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string, 
    private formBuilder: FormBuilder, 
    private router: Router) {
      this.Url = baseUrl;
      this.accountForm = this.formBuilder.group({
        Id: [0],
        Nom: ['', Validators.required],
        Email: ['', [Validators.required, Validators.email]],
        Password: ['', [Validators.required, Validators.minLength(6)]],
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
      formValue["Id"],
      formValue["Nom"],
      formValue["Email"],
      formValue["Password"]);
  }

}
