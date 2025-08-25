// Librerias de angular
import { Routes } from '@angular/router';
// Paginas
import { Home } from './pages/home/home';
import { Test } from './pages/test/test';
import { BoyLookup } from './pages/boy/boy-lookup/boy-lookup';
import { BoyForm } from './pages/boy/boy-form/boy-form';
import { DriverLookup } from './pages/driver/driver-lookup/driver-lookup';
import { DriverForm } from './pages/driver/driver-form/driver-form';
import { BusLookup } from './pages/bus/bus-lookup/bus-lookup';
import { BusForm } from './pages/bus/bus-form/bus-form';

export const routes: Routes = [
  { path: '', component: Home },
  {
    path: 'boy', children: [
      { path: 'form', component: BoyForm },
      { path: 'form/:id', component: BoyForm },
      { path: 'lookup', component: BoyLookup },
    ]
  },
  {
    path: 'driver', children: [
      { path: 'form', component: DriverForm },
      { path: 'form/:id', component: DriverForm },
      { path: 'lookup', component: DriverLookup },
    ]
  },
  {
    path: 'bus', children: [
      { path: 'form', component: BusForm },
      { path: 'form/:id', component: BusForm },
      { path: 'lookup', component: BusLookup },
    ]
  },
  { path: 'test', component: Test },
  { path: '**', redirectTo: '' }
];