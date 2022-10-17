import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router,
              private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if(error) {
          switch(error.status) {
            case 400:
              if(error.error) {
                let responseErrors = [];
                for(const key in error.error.errors) {
                if(error.error.errors[key]) {
                  responseErrors.push(error.error.errors[key]);
                }
                }
                throw responseErrors.flat();
              }
              break;
            case 401:
              this.router.navigate(["/admin/login"]);
              break;
            case 403:
              this.toastr.error("Acesso Negado!")
              break;
            case 404:
              this.router.navigate(["/"]);
              break;
            case 500:
              this.toastr.error("Erro Interno do Servidor!");
              break;
            default:
              this.toastr.warning("Algum erro inesperado acontenceu!");
              break;
          }
        }
        return throwError(error);
      })
    );
  }
}
