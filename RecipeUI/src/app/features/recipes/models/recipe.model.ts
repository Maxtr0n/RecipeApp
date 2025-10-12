export interface Recipe {
    id: string;
    title: string;
    description: string;
    ingredients: string[];
    instructions: string[];
    preparationTime: number;
    cookingTimeInMinutes: number;
    servings: number;
    imageUrls: string[];
    createdAt: Date;
    updatedAt: Date;
}

export interface CreateRecipeDto {
    title: string;
    description: string;
    ingredients: string[];
    instructions: string[];
    preparationTime: number;
    cookingTime: number;
    servings: number;
    imageUrls: string[];
}

export interface UpdateRecipeDto extends Partial<CreateRecipeDto> { }