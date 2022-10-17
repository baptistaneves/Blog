import { Category } from './../../shared/models/category/category';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryApiService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllCategories() : Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + "categories");
  }

  getCategoryById(categoryId:string) : Observable<Category>{
    return this.http.get<Category>(this.baseUrl + "categories/obter-categoria-por-id/" + categoryId);
  }

  createCategory(category:Category) : Observable<Category> {
    return this.http.post<Category>(this.baseUrl + "categories/nova-categoria", category);
  }

  updateCategory(categoryId:string, category:Category) : Observable<any>{
    return this.http.put<any>(this.baseUrl + "categories/actualizar-categoria/" + categoryId, category);
  }

  removeCategory(categoryId:string) {
    return this.http.delete<any>(this.baseUrl + "categories/remover-categoria/" + categoryId); 
  }
}
