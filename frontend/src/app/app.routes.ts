import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Test } from './pages/test/test';
import { BoyLookup } from './pages/boy/lookup/boy-lookup';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'boy', children: [
      // { path: 'crud', component: ChicosCrudComponent },
      { path: 'lookup', component: BoyLookup },
    ]
  },
//   { path: 'choferes', children: [
//       { path: 'crud', component: ChoferesCrudComponent },
//       { path: 'consulta', component: ChoferesConsultaComponent },
//     ]
//   },
//   { path: 'micros', children: [
//       { path: 'crud', component: MicrosCrudComponent },
//       { path: 'consulta', component: MicrosConsultaComponent },
//     ]
//   },
//   { path: 'asociaciones', children: [
//       { path: 'chofer-micro', component: ChoferMicroComponent },
//       { path: 'chicos-micro', component: ChicosMicroComponent },
//       { path: 'consultas', component: AsociacionesConsultaComponent },
//     ]
//   },
  { path: 'test', component: Test },
  {path: '**', redirectTo: ''}
];