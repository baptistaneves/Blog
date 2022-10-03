import { AuthGuard } from './../core/guards/auth.guard';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { AdminUsersComponent } from './admin-users/admin-users.component';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'admin/login', component: AdminLoginComponent},
  {
    path: '',
    component: AdminLayoutComponent,
    children: [
      {path: 'admin', component: AdminHomeComponent, data: {role: ['Admin']}, canActivate: [AuthGuard]},
      {path: 'admin/usuarios', component: AdminUsersComponent, data: {role: ['Admin']}, canActivate: [AuthGuard]}
    ],
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
