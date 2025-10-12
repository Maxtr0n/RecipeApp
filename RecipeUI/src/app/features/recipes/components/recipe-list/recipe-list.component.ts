import { Component, OnInit, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { RecipeService } from '../../services/recipe.service';
import { Recipe } from '../../models/recipe.model';

@Component({
    selector: 'app-recipe-list',
    standalone: true,
    imports: [RouterLink],
    templateUrl: './recipe-list.component.html',
    styleUrls: ['./recipe-list.component.scss']
})
export class RecipeListComponent implements OnInit {
    private readonly recipeService = inject(RecipeService);

    protected readonly recipes = this.recipeService.recipes;
    protected readonly loading = signal(true);
    protected readonly error = signal<string | null>(null);

    ngOnInit(): void {
        this.loadRecipes();
    }

    protected deleteRecipe(id: string): void {
        if (confirm('Are you sure you want to delete this recipe?')) {
            this.recipeService.deleteRecipe(id).subscribe({
                error: (err) => this.error.set(err.message)
            });
        }
    }

    private loadRecipes(): void {
        this.loading.set(true);
        this.error.set(null);

        this.recipeService.getRecipes().subscribe({
            next: () => this.loading.set(false),
            error: (err) => {
                this.error.set(err.message);
                this.loading.set(false);
            }
        });
    }
}