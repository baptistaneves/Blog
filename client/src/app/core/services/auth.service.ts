import { UserApiService } from './../../data/user/user-api.service';
import { User } from '../../shared/models/user/user';
import { map, Observable, of, ReplaySubject } from 'rxjs';
import { JwtTokenService } from './jwt-token.service';
import { Injectable } from '@angular/core';
import { Login } from 'src/app/shared/models/user/login';
import { CreatePublicUser } from 'src/app/shared/models/user/createPublicUser';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSource = new ReplaySubject<User | null>();
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private jwtToken: JwtTokenService,
              private userService: UserApiService) { }


  register(user:CreatePublicUser) {
    return this.userService.createPublicUser(user)
        .pipe(map(user => {
            this.setAuth(user);
            return user;
        }))
  }   

  login(login: Login) : Observable<User> {
    return this.userService.login(login)
        .pipe(map(user => {
            this.setAuth(user);
            return user;
        }))
  }

  logout() {
    this.userService.logout().subscribe();
    this.purgeAuth();
  }

  loadCurrentUser(token:string) : Observable<any> {

    if(token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    return this.userService.getCurrentUser().pipe(
      map(user => {
        this.setAuth(user);
      })
    )

  }

  setAuth(user:User) {
    this.jwtToken.saveToken(user.token);
    this.currentUserSource.next(user);
  }

  purgeAuth() {
    this.jwtToken.destroyToken();
    this.currentUserSource.next(null);  
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
