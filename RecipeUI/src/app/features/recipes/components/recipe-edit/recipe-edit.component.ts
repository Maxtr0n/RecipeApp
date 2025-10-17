import { Component, OnInit, inject, signal } from '@angular/core';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RecipeService } from '../../services/recipe.service';
import { CreateRecipeDto, Recipe, UpdateRecipeDto } from '../../models/recipe.model';

interface RecipeForm {
    title: FormControl<string>;
    description: FormControl<string>;
    preparationTimeInMinutes: FormControl<number>;
    cookingTimeInMinutes: FormControl<number>;
    servings: FormControl<number>;
    imageUrls: FormArray<FormControl<string>>;
    ingredients: FormArray<FormControl<string>>;
    instructions: FormArray<FormControl<string>>;
}

@Component({
    selector: 'app-recipe-edit',
    standalone: true,
    imports: [ReactiveFormsModule],
    templateUrl: './recipe-edit.component.html',
    styleUrls: ['./recipe-edit.component.scss']
})
export class RecipeEditComponent implements OnInit {
    private readonly recipeService = inject(RecipeService);
    private readonly route = inject(ActivatedRoute);
    private readonly router = inject(Router);

    protected readonly loading = signal(true);
    protected readonly error = signal<string | null>(null);
    protected readonly isEdit = signal(false);

    protected form = new FormGroup<RecipeForm>({
        title: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
        description: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
        preparationTimeInMinutes: new FormControl(0, { nonNullable: true, validators: [Validators.min(0)] }),
        cookingTimeInMinutes: new FormControl(0, { nonNullable: true, validators: [Validators.min(0)] }),
        servings: new FormControl(1, { nonNullable: true, validators: [Validators.min(1)] }),
        imageUrls: new FormArray<FormControl<string>>([]),
        ingredients: new FormArray<FormControl<string>>([]),
        instructions: new FormArray<FormControl<string>>([])
    });

    protected get ingredients() {
        return this.form.get('ingredients') as FormArray<FormControl<string>>;
    }

    protected get instructions() {
        return this.form.get('instructions') as FormArray<FormControl<string>>;
    }

    protected get imageUrls() {
        return this.form.get('imageUrls') as FormArray<FormControl<string>>;
    }

    ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get('id');
        if (id) {
            this.isEdit.set(true);
            this.loadRecipe(id);
        } else {
            this.loading.set(false);
            this.addIngredient();
            this.addInstruction();
        }
    }

    protected onSubmit(): void {
        if (this.form.invalid) return;

        const recipe: CreateRecipeDto = {
            title: this.form.value.title!,
            description: this.form.value.description!,
            preparationTimeInMinutes: this.form.value.preparationTimeInMinutes!,
            cookingTimeInMinutes: this.form.value.cookingTimeInMinutes!,
            servings: this.form.value.servings!,
            imageUrls: this.imageUrls.value,
            ingredients: this.ingredients.value,
            instructions: this.instructions.value
        };

        const id = this.route.snapshot.paramMap.get('id');
        const request = id ?
            this.recipeService.updateRecipe(id, recipe as UpdateRecipeDto) :
            this.recipeService.createRecipe(recipe);

        request.subscribe({
            next: () => this.router.navigate(['/recipes']),
            error: (err) => this.error.set(err.message)
        });
    }

    protected addIngredient(): void {
        this.ingredients.push(new FormControl('', { nonNullable: true }));
    }

    protected removeIngredient(index: number): void {
        this.ingredients.removeAt(index);
    }

    protected addInstruction(): void {
        this.instructions.push(new FormControl('', { nonNullable: true }));
    }

    protected removeInstruction(index: number): void {
        this.instructions.removeAt(index);
    }

    protected addImageUrl(): void {
        this.imageUrls.push(new FormControl('', { nonNullable: true }));
    }

    protected removeImageUrl(index: number): void {
        this.imageUrls.removeAt(index);
    }

    protected cancel(): void {
        this.router.navigate(['/recipes']);
    }

    private loadRecipe(id: string): void {
        this.loading.set(true);
        this.error.set(null);

        this.recipeService.getRecipe(id).subscribe({
            next: (recipe) => {
                this.form.patchValue({
                    title: recipe.title,
                    description: recipe.description,
                    preparationTimeInMinutes: recipe.preparationTimeInMinutes,
                    cookingTimeInMinutes: recipe.cookingTimeInMinutes,
                    servings: recipe.servings,
                    imageUrls: []
                });

                recipe.imageUrls.forEach(url => {
                    this.imageUrls.push(new FormControl(url, { nonNullable: true }));
                });

                recipe.ingredients.forEach(ingredient => {
                    this.ingredients.push(new FormControl(ingredient, { nonNullable: true }));
                });

                recipe.instructions.forEach(instruction => {
                    this.instructions.push(new FormControl(instruction, { nonNullable: true }));
                });

                this.loading.set(false);
            },
            error: (err) => {
                this.error.set(err.message);
                this.loading.set(false);
            }
        });
    }
}