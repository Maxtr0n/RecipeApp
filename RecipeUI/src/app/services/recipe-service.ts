import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class RecipeService {
    BASE_URL: string = "https://localhost:7171";

    constructor(private readonly httpClient: HttpClient) { }

    public getAllRecipes(): Observable<Recipe> {
        return this.httpClient.get<Recipe>(this.BASE_URL + "/asd");
    }
}
