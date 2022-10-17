import { PaginationParams } from './../../../shared/common/paginationParams';
import { ToastrService } from 'ngx-toastr';
import { PostDto } from './../../../shared/models/post/postDto';
import { Component, OnInit } from '@angular/core';
import { PostApiService } from 'src/app/data/post/post-api.service';
import { Pagination } from 'src/app/shared/common/pagination';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html'
})
export class PostsComponent implements OnInit {
  posts: PostDto[];
  pagination: Pagination;
  params: PaginationParams;
  errors: string[] = [];

  constructor(private postService: PostApiService,
              private toastr:ToastrService) { }

  ngOnInit(): void {
    this.params = new PaginationParams();
    this.loadPosts();
  }

  loadPosts() {
    this.postService.getAllposts(this.params).subscribe(response=> {
      this.posts = response.result;
      this.pagination = response.pagination;
    })
  }

  onPageChange(event:any) {
    this.params.page = event;
    this.loadPosts();
  }

  remove(postId:string) {
    if(confirm("Deseja realmente remover esta notícia?")) {
      this.postService.remove(postId).subscribe(() => {
        this.loadPosts();
        this.toastr.success("Notícia removida com sucesso.");
      }, errors => this.errors = errors)
    }
  }
}
