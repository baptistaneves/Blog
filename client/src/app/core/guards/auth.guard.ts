import { JwtTokenService } from './../services/jwt-token.service';
import { AuthService } from 'src/app/core/services/auth.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService,
              private jwtService: JwtTokenService,
              private route: Router){}

  canActivate(next: ActivatedRouteSnapshot): boolean {

      if(this.jwtService.getToken() != null) {
        let roles = next.data['role'] as Array<string>;
        if(roles) {
          if(this.authService.roleMatch(roles)) return true;
          else {
            this.route.navigate(['/admin/login']);
            return false;
          }
        }
        return true;
      }

      this.route.navigate(['/admin/login']);
      return false;
  }
  
}
