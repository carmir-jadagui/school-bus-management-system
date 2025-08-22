// Librerias de angular
import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, provideHttpClient, withFetch, withInterceptorsFromDi } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
// Otros
import { routes } from './app.routes';
import { ApiInterceptor } from './core/interceptos/api.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes), provideClientHydration(withEventReplay()),
    provideHttpClient(
      withFetch(),                // usa fetch en lugar de XHR
      withInterceptorsFromDi()    // permite interceptores globales
    ),
    { provide: HTTP_INTERCEPTORS, useClass: ApiInterceptor, multi: true },
    provideAnimations()
  ]
};