import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Test } from './pages/test/test';

export const routes: Routes = [
  { path: '', component: Home },
//   { path: 'chicos', children: [
//       { path: 'crud', component: ChicosCrudComponent },
//       { path: 'consulta', component: ChicosConsultaComponent },
//     ]
//   },
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