import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { MatInputModule } from '@angular/material/input';
import { CounterComponent } from './counter/counter.component';
import { MatProgressSpinnerModule, MatIconModule, MatSnackBar, MatSnackBarModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CreateAccountComponent } from './create-account/create-account.component';
import { LoginComponent } from './login/login.component';
import { AuthService } from './services/auth.service';
import { GameComponent } from './game/game.component';
import { GameService } from './services/game.service';
import { AlertModule, AlertConfig } from 'ngx-bootstrap/alert';
import { ModalModule, BsModalService } from 'ngx-bootstrap/modal';
import { CollapseModule, TooltipModule, ProgressbarModule } from 'ngx-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    CreateAccountComponent,
    LoginComponent,
    GameComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    MatProgressSpinnerModule,
    BrowserAnimationsModule,
    MatSnackBarModule,
    AlertModule,
    ModalModule.forRoot(),
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    ProgressbarModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'game', component: GameComponent },
      { path: 'create-account', component: CreateAccountComponent },
      { path: 'login', component: LoginComponent }
    ])
  ],
  providers: [AuthService, GameService, AlertConfig, BsModalService],
  bootstrap: [AppComponent]
})
export class AppModule { }
