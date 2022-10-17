import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { UserRole } from './../../shared/models/user/userRole';
import { UserApiService } from './../../data/user/user-api.service';
import { UserProfile } from 'src/app/shared/models/user/userProfile';
import { Modal } from 'src/app/shared/common/modal';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html'
})
export class AdminUsersComponent implements OnInit {
  errors: string[] = [];
  removeErrors: string[] = [];
  identityId:string;
  roles: UserRole[];
  users: UserProfile[];
  userAddForm: FormGroup;
  userUpdateForm: FormGroup;

  constructor(private userService: UserApiService,
              private formBuilder: FormBuilder,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadUsers();
    this.initializeAddForm();
    this.initializeUpdateForm();
  }

  loadUsers() {
    this.userService.getAdminUsers().subscribe(users => {
      this.users = users
    })
  }

  loadRoles() {
    this.userService.getUserRoles().subscribe(roles => {
      this.roles = roles;
    })
  }

  initializeAddForm() {
    this.userAddForm = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      phone: ['', [Validators.required]],
      emailAddress: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      role: ['', [Validators.required]]
    });
  }

  emptyFields() {
    this.userAddForm.reset();
  }

  get f() { return this.userAddForm.controls }

  create() {
    this.userService.createAdminUser(this.userAddForm.value).subscribe(() => {
      Modal.closeModal('bntClose');
      this.emptyFields();
      this.loadUsers();
      this.toastr.success("Usuário cadastrado com sucesso!");
    }, errors => this.errors = errors)
  }

  initializeUpdateForm() {
    this.userUpdateForm = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      phone: ['', [Validators.required]],
      emailAddress: ['', [Validators.required, Validators.email]]
    });
  }

  fillFields(user:UserProfile) {
    this.identityId = user.identityId;
    this.userUpdateForm.controls['firstName'].setValue(user.basicInfo.firstName);
    this.userUpdateForm.controls['lastName'].setValue(user.basicInfo.lastName);
    this.userUpdateForm.controls['phone'].setValue(user.basicInfo.phone);
    this.userUpdateForm.controls['emailAddress'].setValue(user.basicInfo.emailAddress);
  }

  get fu() { return this.userUpdateForm.controls }

  update() {
    this.userService.updateUserProfile(this.identityId, this.userUpdateForm.value).subscribe(()=> {
      Modal.closeModal('bntEditClose');
      this.emptyFields();
      this.loadUsers();
      this.toastr.success("Usuário actualizado com sucesso!");
    }, errors =>  this.errors = errors)
  }

  remove(userId:string) {
    if(confirm("Deseja realmente excluir este úsuario?")) {
      this.userService.removeUser(userId).subscribe(()=> {
        this.loadUsers();
        this.toastr.success("Usuário removido com sucesso!");
      }, errors => this.removeErrors = errors)
    }
  }

}
