import { PostApiService } from './post/post-api.service';
import { CategoryApiService } from './category/category-api.service';
import { UserApiService } from './user/user-api.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    UserApiService,
    CategoryApiService,
    PostApiService
  ]
})
export class DataModule { }
