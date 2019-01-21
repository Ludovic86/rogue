import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  constructor(private authService: AuthService) {}

  ngOnInit(){
    // this.authService.authCheck();
    // this.authService.emitAuthSubject();
  }
}
