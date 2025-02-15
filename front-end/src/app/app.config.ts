import { ApplicationConfig, provideZoneChangeDetection, importProvidersFrom } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, HttpClientModule, withFetch } from '@angular/common/http';
//import { provideCharts, withDefaultRegisterables } from 'ng2-charts';
import { ContaService } from './services/conta.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes, withComponentInputBinding()),
    provideAnimationsAsync(),
    //provideHttpClient(), provideCharts(withDefaultRegisterables()),
    ContaService, importProvidersFrom(HttpClientModule), provideHttpClient(withFetch())
  ]
};
