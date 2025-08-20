import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Test } from './pages/test/test';

export const routes: Routes = [
    {path: '', component: Home },
    {path: 'Test', component: Test},
    {path: '**', redirectTo: ''}
];