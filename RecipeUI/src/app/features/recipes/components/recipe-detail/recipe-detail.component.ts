import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { RecipeService } from '../../services/recipe.service';
import { Recipe } from '../../models/recipe.model';

@Component({
    selector: 'app-recipe-detail',
    standalone: true,
    imports: [RouterLink],
    templateUrl: './recipe-detail.component.html',
    styleUrls: ['./recipe-detail.component.scss']
})
export class RecipeDetailComponent implements OnInit {
    private readonly recipeService = inject(RecipeService);
    private readonly route = inject(ActivatedRoute);
    private readonly router = inject(Router);

    protected readonly recipe = signal<Recipe | null>(null);
    protected readonly loading = signal(true);
    protected readonly error = signal<string | null>(null);

    ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get('id');
        if (!id) {
            this.error.set('Recipe ID not found');
            return;
        }

        this.loadRecipe(id);
    }

    protected deleteRecipe(): void {
        if (!this.recipe() || !confirm('Are you sure you want to delete this recipe?')) {
            return;
        }

        this.recipeService.deleteRecipe(this.recipe()!.id).subscribe({
            next: () => this.router.navigate(['/recipes']),
            error: (err) => this.error.set(err.message)
        });
    }

    private loadRecipe(id: string): void {
        this.loading.set(true);
        this.error.set(null);

        this.recipeService.getRecipe(id).subscribe({
            next: (recipe) => {
                this.recipe.set(recipe);
                this.loading.set(false);
            },
            error: (err) => {
                this.error.set(err.message);
                this.loading.set(false);
            }
        });
    }
}