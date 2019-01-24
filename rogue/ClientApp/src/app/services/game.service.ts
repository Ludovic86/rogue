import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute } from "@angular/router";

@Injectable()
export class GameService{

    Url: string;

    constructor (private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute){
        this.Url = baseUrl;
    }

    

}