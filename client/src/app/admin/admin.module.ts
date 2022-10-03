import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminUsersComponent } from './admin-users/admin-users.component';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AdminLayoutComponent,
    AdminHomeComponent,
    AdminUsersComponent,
    AdminLoginComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AdminRoutingModule
  ],
  exports: [
    AdminLayoutComponent
  ]
})
export class AdminModule { }
