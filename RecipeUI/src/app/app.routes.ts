import { Routes } from '@angular/router';
import { coreRoutes } from './core/core.routes';

export const routes: Routes = [
    ...coreRoutes,
    {
        path: 'recipes',
        loadChildren: () => import('./features/recipes/recipes.routes')
    }
];
