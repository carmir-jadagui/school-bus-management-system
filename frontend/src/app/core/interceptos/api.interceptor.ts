import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environments';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Agregar base URL si no es absoluta
    const apiReq = req.url.startsWith('http')
      ? req
      : req.clone({ url: environment.apiUrlBase + req.url });

    // Agregar headers
    const headersReq = apiReq.clone({
      setHeaders: {
        'Content-Type': 'application/json'
      }
    });

    return next.handle(headersReq);
  }
}