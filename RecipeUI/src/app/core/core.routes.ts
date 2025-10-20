import { Routes } from '@angular/router';
import { MainLayout } from './layout/main-layout/main-layout';
import { Home } from './components/home/home';

export const coreRoutes: Routes = [
    {
        path: '',
        component: MainLayout,
        children: [
            { path: '', component: Home },
        ],
    },
];