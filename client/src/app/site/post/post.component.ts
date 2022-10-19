import { ToastrService } from 'ngx-toastr';
import { AuthService } from './../../core/services/auth.service';
import { Observable, map } from 'rxjs';
import { PostCommentDto } from './../../shared/models/post/postCommentDto';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { PostDto } from './../../shared/models/post/postDto';
import { PostApiService } from './../../data/post/post-api.service';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from 'src/app/shared/models/user/user';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html'
})
export class PostComponent implements OnInit {

  post: PostDto;
  postComments: PostCommentDto[];
  postId:string;
  pathImage = environment.pathImage;
  postCommentForm:FormGroup;
  errors:string[] = [];
  currentUser$: Observable<User | null>;

  constructor(private postService:PostApiService,
              private authService:AuthService,
              private actRoute:ActivatedRoute,
              private formBuilder:FormBuilder,
              private toastr:ToastrService) { }

  ngOnInit(): void {
    this.currentUser$ = this.authService.currentUser$;
    this.postId = this.actRoute.snapshot.params['postId'];
    this.loadPost();
    this.loadPostComments();
    this.initializeForm();
  }

  initializeForm() {
    this.postCommentForm = this.formBuilder.group({
      text: ['', [Validators.required]]
    })
  }

  get f() { return this.postCommentForm.controls }

  loadPost() {
    this.postService.getPostById(this.postId).subscribe(response => {
      this.post = response;
    })
  }

  loadPostComments()  {
    this.postService.getAllPostsComments(this.postId).subscribe(response => {
      this.postComments = response;
    })
  }

  addComment() {
    this.postService.addPostComment(this.post.postId, this.postCommentForm.value)
      .subscribe(response => {
        this.loadPostComments();
      }, errors => this.errors = this.errors);
  }

}
