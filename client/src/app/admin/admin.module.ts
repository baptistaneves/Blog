import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminUsersComponent } from './admin-users/admin-users.component';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { NgModule } from '@angular/core';
import { AdminCategoryComponent } from './admin-category/admin-category.component';
import { PostsComponent } from './admin-posts/posts/posts.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { CreatePostComponent } from './admin-posts/create-post/create-post.component';
import { NgxSummernoteModule } from 'ngx-summernote';
import { UpdatePostComponent } from './admin-posts/update-post/update-post.component';


@NgModule({
  declarations: [
    AdminLayoutComponent,
    AdminHomeComponent,
    AdminUsersComponent,
    AdminLoginComponent,
    AdminCategoryComponent,
    PostsComponent,
    CreatePostComponent,
    UpdatePostComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    NgxSummernoteModule,
    AdminRoutingModule
  ],
  exports: [
    AdminLayoutComponent
  ]
})
export class AdminModule { }
