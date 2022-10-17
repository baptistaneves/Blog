import { PaginationParams } from './../../shared/common/paginationParams';
import { PostDto } from 'src/app/shared/models/post/postDto';
import { Component, OnInit } from '@angular/core';
import { PostApiService } from 'src/app/data/post/post-api.service';
import { Pagination } from 'src/app/shared/common/pagination';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {

  posts: PostDto[];
  params: PaginationParams;
  pagination: Pagination;

  constructor(private postService: PostApiService) { }

  ngOnInit(): void {
    this.params = new PaginationParams();
    this.loadPosts();
  }

  loadPosts() {
    this.postService.getAllposts(this.params).subscribe(response => {
      this.posts = response.result;
      this.pagination = response.pagination;
    });
  }

  onPageChange(event:any) {
    this.params.page = event;
    this.loadPosts();
  }
}
