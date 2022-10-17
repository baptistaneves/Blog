import { User } from 'src/app/shared/models/user/user';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html'
})
export class AdminLayoutComponent implements OnInit {

  currentUser$: Observable<User | null>;

  constructor(private authService: AuthService,
              private router: Router) { }

  ngOnInit(): void {
    this.currentUser$ = this.authService.currentUser$;
  }

  logout() {
    this.authService.logout();
    this.router.navigate(["/admin/login"]);
  }
}
