import { Category } from './../../shared/models/category/category';
import { ToastrService } from 'ngx-toastr';
import { CategoryApiService } from './../../data/category/category-api.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Modal } from 'src/app/shared/common/modal';

@Component({
  selector: 'app-admin-category',
  templateUrl: './admin-category.component.html'
})
export class AdminCategoryComponent implements OnInit {

  errors: string[] = [];
  removeErrors: string[] = [];
  categories: Category[];
  categoryId: string;
  categoryForm: FormGroup;

  addFlag:boolean;
  updateFlag:boolean; 

  constructor(private categoryService:CategoryApiService,
              private router:Router,
              private toastr:ToastrService,
              private formBuilder:FormBuilder) { }

  ngOnInit(): void {
    this.loadCategories();
    this.initializeForm();
  }

  loadCategories() {
    this.categoryService.getAllCategories().subscribe(result => {
      this.categories = result;
    })
  }

  initializeForm() {
    this.categoryForm = this.formBuilder.group({
      description: ['', [Validators.required, Validators.minLength(4)]]
    });
  }

  get f() { return this.categoryForm.controls }

  fillFields(category:Category) {
    this.setUpdateFlag();
    this.categoryId = category.categoryId;
    this.categoryForm.controls['description'].setValue(category.description);
  }

  reseteFields() {
    this.categoryForm.reset();
  }

  setCreateFlag() {
    this.addFlag = true;
    this.updateFlag = false;
  }

  setUpdateFlag() {
    this.addFlag = false;
    this.updateFlag = true;
  }

  create() {
    this.categoryService.createCategory(this.categoryForm.value).subscribe(result => {
      Modal.closeModal('bntClose');
      this.reseteFields();
      this.loadCategories();
      this.toastr.success("Categoria criada com sucesso!");
    }, errors => this.errors = errors)
  }

  update() {
    this.categoryService.updateCategory(this.categoryId, this.categoryForm.value).subscribe(result => {
      Modal.closeModal('bntClose');
      this.reseteFields();
      this.loadCategories();
      this.toastr.success("Categoria actualizada com sucesso!");
    }, errors => this.errors = errors)
  }

  remove(categoryId:string) {
    if(confirm("Deseja realmente remover esta categoria?")) {
      this.categoryService.removeCategory(categoryId).subscribe(result=> {
          this.loadCategories();
          this.toastr.success("Categoria removida com sucesso!");
        }, errors => this.removeErrors = errors)
      }
  }

}
