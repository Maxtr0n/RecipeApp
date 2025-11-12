import { RecipeListComponent } from './components/recipe-list/recipe-list.component';
import { RecipeDetailComponent } from './components/recipe-detail/recipe-detail.component';
import { RecipeEditComponent } from './components/recipe-edit/recipe-edit.component';

export default [
    {
        path: '',
        children: [
            {
                path: '',
                component: RecipeListComponent
            },
            {
                path: 'new',
                component: RecipeEditComponent
            },
            {
                path: ':id',
                component: RecipeDetailComponent
            },
            {
                path: ':id/edit',
                component: RecipeEditComponent
            }
        ]
    }
];