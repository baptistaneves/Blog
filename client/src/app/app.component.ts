import { JwtTokenService } from './core/services/jwt-token.service';
import { AuthService } from 'src/app/core/services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'Tech Blog';

  constructor(private authService:AuthService,
              private tokenService:JwtTokenService) {}

  ngOnInit(): void {
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    this.authService.loadCurrentUser(this.tokenService.getToken()).subscribe(() => {

    }, error => {
      console.log(error);
    })
  }

}
