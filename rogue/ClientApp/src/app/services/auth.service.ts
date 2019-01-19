import { CookieService } from 'ngx-cookie-service';
import { OnInit } from '@angular/core';
import { Injectable } from '@angular/compiler/src/core';

export class AuthService implements OnInit{
    isAuth: boolean;


    // constructor (private cookieService: CookieService) {}

    ngOnInit() : void{
        //this.isAuth = this.cookieService.check('test');
    }
}