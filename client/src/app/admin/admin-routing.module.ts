import { UpdatePostComponent } from './admin-posts/update-post/update-post.component';
import { CreatePostComponent } from './admin-posts/create-post/create-post.component';
import { PostsComponent } from './admin-posts/posts/posts.component';
import { AdminCategoryComponent } from './admin-category/admin-category.component';
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
      {path: 'admin', component: AdminHomeComponent, data: {role: ['Admin', 'Editor']}, canActivate: [AuthGuard]},
      {path: 'admin/usuarios', component: AdminUsersComponent, data: {role: ['Admin', 'Editor']}, canActivate: [AuthGuard]},
      {path: 'admin/categorias', component: AdminCategoryComponent, data: {role: ['Admin', 'Editor']}, canActivate: [AuthGuard]},
      {path: 'admin/noticias', component: PostsComponent, data: {role: ['Admin', 'Editor']}, canActivate: [AuthGuard]},
      {path: 'admin/nova-noticia', component: CreatePostComponent, data: {role: ['Admin', 'Editor']}, canActivate: [AuthGuard]},
      {path: 'admin/actualizar-noticia/:postId', component: UpdatePostComponent, data: {role: ['Admin', 'Editor']}, canActivate: [AuthGuard]}
    ],
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
