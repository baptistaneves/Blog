import { UserApiService } from './../../data/user/user-api.service';
import { User } from '../../shared/models/user/user';
import { map, Observable } from 'rxjs';
import { JwtTokenService } from './jwt-token.service';
import { Injectable } from '@angular/core';
import { Login } from 'src/app/shared/models/user/login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private jwtToken: JwtTokenService,
              private userService: UserApiService) { }

  login(login: Login) : Observable<User> {
    return this.userService.login(login)
        .pipe(map(user => {
            this.setAuth(user)
            return user;
        }))
  }

  setAuth(user:User) {
    this.jwtToken.saveToken(user.token);
  }

  logout() {
    this.purgeAuth();
  }

  purgeAuth() {
    this.jwtToken.destroyToken();
  }

  roleMatch(roles: Array<string>) : boolean {
    let isMatch: boolean = false;
    let payload = JSON.parse(this.decodefyToken(this.jwtToken.getToken()));

    roles.forEach(element => {
      if(element == payload.role) isMatch = true;
    });

    return isMatch;
  }

  decodefyToken(token:string) {
    return atob(token.split('.')[1]);
  }

}
