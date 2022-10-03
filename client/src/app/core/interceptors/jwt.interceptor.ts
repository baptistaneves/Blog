import { JwtTokenService } from './../services/jwt-token.service';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private jwtService: JwtTokenService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    
    const headerConfig: any = {
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    }

    let token = this.jwtService.getToken();
    if(token) headerConfig['Authorization'] = `Bearer ${token}`;

    let req = request.clone({setHeaders: headerConfig});

    return next.handle(req);
  }
}
