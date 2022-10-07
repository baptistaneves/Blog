import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html'
})
export class AdminLoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private autService:AuthService,
              private formBuilder:FormBuilder,
              private route:Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.loginForm = this.formBuilder.group({
      userName: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  get f() { return this.loginForm.controls; }

  login() {
    this.autService.login(this.loginForm.value).subscribe(resp => {
      if(resp) this.route.navigate(['/admin']);
    }, error => {
      console.log(error);
    })
  }

}
