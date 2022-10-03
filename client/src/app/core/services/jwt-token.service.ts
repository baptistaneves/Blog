import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class JwtTokenService {

  getToken(): string {
    return window.localStorage["token"];
  }

  saveToken(token:string) {
    window.localStorage.setItem("token", token);
  }

  destroyToken() {
    window.localStorage.removeItem("token");
    window.localStorage.removeItem("user");
  }
}
