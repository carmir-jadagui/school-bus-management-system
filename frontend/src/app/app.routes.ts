// Librerias de angular
import { Routes } from '@angular/router';
// Paginas
import { Home } from './pages/home/home';
import { Test } from './pages/test/test';
import { BoyLookup } from './pages/boy/lookup/boy-lookup';
import { DriverLookup } from './pages/driver/lookup/driver-lookup';
import { BusLookup } from './pages/bus/lookup/bus-lookup';

export const routes: Routes = [
  { path: '', component: Home },
  {
    path: 'boy', children: [
      // { path: 'crud', component: ChicosCrudComponent },
      { path: 'lookup', component: BoyLookup },
    ]
  },
  {
    path: 'driver', children: [
      // { path: 'crud', component: ChoferesCrudComponent },
      { path: 'lookup', component: DriverLookup },
    ]
  },
  {
    path: 'bus', children: [
      // { path: 'crud', component: MicrosCrudComponent },
      { path: 'lookup', component: BusLookup },
    ]
  },
  //   { path: 'asociaciones', children: [
  //       { path: 'chofer-micro', component: ChoferMicroComponent },
  //       { path: 'chicos-micro', component: ChicosMicroComponent },
  //       { path: 'consultas', component: AsociacionesConsultaComponent },
  //     ]
  //   },
  { path: 'test', component: Test },
  { path: '**', redirectTo: '' }
];