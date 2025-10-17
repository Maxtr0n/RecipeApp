export interface Recipe {
    id: string;
    title: string;
    description: string;
    ingredients: string[];
    instructions: string[];
    preparationTimeInMinutes: number;
    cookingTimeInMinutes: number;
    servings: number;
    imageUrls: string[];
    createdAtUtc: Date;
    updatedAtUtc: Date;
}

export interface CreateRecipeDto {
    title: string;
    description: string;
    ingredients: string[];
    instructions: string[];
    preparationTimeInMinutes: number;
    cookingTimeInMinutes: number;
    servings: number;
    imageUrls: string[];
}

export interface UpdateRecipeDto extends Partial<CreateRecipeDto> { }