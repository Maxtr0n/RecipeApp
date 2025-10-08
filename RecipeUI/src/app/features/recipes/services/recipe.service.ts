import { Injectable, signal } from '@angular/core';
import { Observable, tap, catchError } from 'rxjs';
import { HttpBaseService } from '../../../shared/services/http-base.service';
import { ENDPOINTS } from '../../../shared/constants/api.constants';
import { Recipe, CreateRecipeDto, UpdateRecipeDto } from '../models/recipe.model';

@Injectable({
    providedIn: 'root'
})
export class RecipeService extends HttpBaseService {
    private readonly recipesSignal = signal<Recipe[]>([]);
    public readonly recipes = this.recipesSignal.asReadonly();

    getRecipes(): Observable<Recipe[]> {
        return this.http.get<Recipe[]>(ENDPOINTS.RECIPES)
            .pipe(
                tap(recipes => this.recipesSignal.set(recipes)),
                catchError(this.handleError)
            );
    }

    getRecipe(id: string): Observable<Recipe> {
        return this.http.get<Recipe>(`${ENDPOINTS.RECIPES}/${id}`)
            .pipe(
                catchError(this.handleError)
            );
    }

    createRecipe(recipe: CreateRecipeDto): Observable<Recipe> {
        return this.http.post<Recipe>(ENDPOINTS.RECIPES, recipe)
            .pipe(
                tap(newRecipe => {
                    this.recipesSignal.update(recipes => [...recipes, newRecipe]);
                }),
                catchError(this.handleError)
            );
    }

    updateRecipe(id: string, recipe: UpdateRecipeDto): Observable<Recipe> {
        return this.http.put<Recipe>(`${ENDPOINTS.RECIPES}/${id}`, recipe)
            .pipe(
                tap(updatedRecipe => {
                    this.recipesSignal.update(recipes =>
                        recipes.map(r => r.id === id ? updatedRecipe : r)
                    );
                }),
                catchError(this.handleError)
            );
    }

    deleteRecipe(id: string): Observable<void> {
        return this.http.delete<void>(`${ENDPOINTS.RECIPES}/${id}`)
            .pipe(
                tap(() => {
                    this.recipesSignal.update(recipes =>
                        recipes.filter(recipe => recipe.id !== id)
                    );
                }),
                catchError(this.handleError)
            );
    }
}