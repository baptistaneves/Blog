import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { UserApiService } from './../../data/user/user-api.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-resgisto',
  templateUrl: './resgisto.component.html'
})
export class ResgistoComponent implements OnInit {

  registerForm: FormGroup;
  errors:string[] = [];

  constructor(private userService:UserApiService,
              private authService:AuthService,
              private formBuilder:FormBuilder,
              private router:Router,
              private toastr:ToastrService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      phone: ['', [Validators.required]],
      emailAddress: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  get f() { return this.registerForm.controls }

  register() {
    this.authService.register(this.registerForm.value).subscribe((user) => {
      this.toastr.success("Registo realizado com sucesso!");
      this.router.navigate(["/"]);
    }, errors => this.errors = errors);
  }
}
