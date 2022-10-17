import { environment } from 'src/environments/environment';
import { ToastrService } from 'ngx-toastr';
import { PostApiService } from 'src/app/data/post/post-api.service';
import { CategoryApiService } from './../../../data/category/category-api.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Category } from 'src/app/shared/models/category/category';
import { Router } from '@angular/router';
import { HttpEventType, HttpErrorResponse } from '@angular/common/http';

declare var $:any;

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html'
})
export class CreatePostComponent implements OnInit {
  postForm:FormGroup;
  categories: Category[];
  errors: string[] = [];
  progress: number;
  message:string;
  pathImage = environment.pathImage;
  imageFileName:string;

  @Output() public onUploadFinished = new EventEmitter();

  constructor(private formBuilder:FormBuilder,
              private categoryService:CategoryApiService,
              private postService:PostApiService,
              private router:Router,
              private toastr:ToastrService) { }

  ngOnInit(): void {
    this.loadCategories();
    this.initializeForm();

  }

  loadCategories() {
    this.categoryService.getAllCategories().subscribe(response => {
      this.categories = response;
    })
  }

  initializeForm() {
    this.postForm = this.formBuilder.group({
      title: ['', [Validators.required]],
      categoryId: ['', [Validators.required]],
      content: ['', [Validators.required]],
      image: ['', [Validators.required]],
    });
  }

  get f() { return this.postForm.controls }

  create() {
    this.postForm.value["image"] = this.imageFileName;
    
    this.postService.create(this.postForm.value).subscribe(result => {
      this.router.navigate(['/admin/noticias']);
      this.toastr.success("Notícia criada com sucesso");
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
        }
      }, error: (err: HttpErrorResponse) => console.log(err)
    });
  }

}
