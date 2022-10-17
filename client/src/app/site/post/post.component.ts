import { ActivatedRoute } from '@angular/router';
import { PostDto } from './../../shared/models/post/postDto';
import { PostApiService } from './../../data/post/post-api.service';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html'
})
export class PostComponent implements OnInit {

  post: PostDto;
  postId:string;
  pathImage = environment.pathImage;

  constructor(private postService:PostApiService,
              private actRoute:ActivatedRoute) { }

  ngOnInit(): void {
    this.postId = this.actRoute.snapshot.params['postId'];
    this.loadPost();
  }

  loadPost() {
    this.postService.getPostById(this.postId).subscribe(response => {
      this.post = response;
    })
  }

}
