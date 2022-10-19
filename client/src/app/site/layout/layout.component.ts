import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/core/services/auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Modal } from 'src/app/shared/common/modal';
import { User } from 'src/app/shared/models/user/user';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html'
})
export class LayoutComponent implements OnInit {

  loginForm:FormGroup;
  errors:string[] = [];
  currentUser$: Observable<User | null>;

  constructor(private authService:AuthService,
              private formBuilder:FormBuilder,
              private router:Router,
              private toastr:ToastrService) { }

  ngOnInit(): void {
    this.currentUser$ = this.authService.currentUser$;
    this.initializeForm();
  }
  
  initializeForm() {
    this.loginForm = this.formBuilder.group({
      userName: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  resetForm() {
    this.loginForm.reset();
  }

  get f() { return this.loginForm.controls; }

  login() {
    this.authService.login(this.loginForm.value).subscribe(resp => {
      this.resetForm();
      Modal.closeModal("bntClose");
      this.toastr.success("Login realizado com sucesso");
      this.router.navigate(['/']);
    }, errors => this.errors = errors)
  }

  logout() {
    this.authService.logout();
    this.router.navigate(["/"]);
  }

}
