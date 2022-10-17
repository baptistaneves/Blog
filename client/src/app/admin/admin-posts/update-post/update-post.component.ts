import { environment } from 'src/environments/environment';
import { PostDto } from 'src/app/shared/models/post/postDto';
import { CategoryApiService } from './../../../data/category/category-api.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Category } from 'src/app/shared/models/category/category';
import { PostApiService } from 'src/app/data/post/post-api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-update-post',
  templateUrl: './update-post.component.html'
})
export class UpdatePostComponent implements OnInit {

  postForm:FormGroup;
  categories: Category[];
  post:PostDto;
  errors: string[] = [];
  progress: number;
  message:string;
  pathImage = environment.pathImage;
  imageFileName:string;
  postId:string;

  @Output() public onUploadFinished = new EventEmitter();

  constructor(private formBuilder:FormBuilder,
              private categoryService:CategoryApiService,
              private postService:PostApiService,
              private router:Router,
              private actRoute:ActivatedRoute,
              private toastr:ToastrService) { }

  ngOnInit(): void {
    this.postId = this.actRoute.snapshot.params['postId'];
    this.loadPost();
    this.loadCategories();
    this.initializeForm();
  }

  loadCategories() {
    this.categoryService.getAllCategories().subscribe(response => {
      this.categories = response;
    })
  }

  loadPost() {
    this.postService.getPostById(this.postId).subscribe(response => {
      this.fillFields(response);
    })
  }

  fillFields(post:PostDto) {
    this.postForm.controls['title'].setValue(post.title);
    this.postForm.controls['content'].setValue(post.content);
    this.postForm.controls['categoryId'].setValue(post.categoryId);
    this.imageFileName = post.image;
  }

  initializeForm() {
    this.postForm = this.formBuilder.group({
      title: ['', [Validators.required]],
      categoryId: ['', [Validators.required]],
      content: ['', [Validators.required]],
      image: ['', []],
    });
  }

  get f() { return this.postForm.controls }

  update() {
    this.postService.update(this.postId, this.postForm.value).subscribe(result => {
      this.router.navigate(['/admin/noticias']);
      this.toastr.success("Notícia actualizada com sucesso");
    }, errors => this.errors = errors);
  }

  uploadFile = (event:any) => {

    var file = event.target.files[0];
    const formData:FormData = new FormData();
    formData.append('file', file, file.name);

    this.postService.uploadImage(formData).subscribe({
      next: (event: any) => {
        if(event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if(event.type === HttpEventType.Response){
          this.message = "Imagem carregada com sucesso.";
          this.onUploadFinished.emit(event.body);
          this.imageFileName = event.body;
          this.postForm.value["image"] = this.imageFileName;
        }
      }, error: (err: HttpErrorResponse) => console.log(err)
    });
  }


}
