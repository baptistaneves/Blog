import { PaginationHeader } from './../../shared/common/paginationHeader';
import { HttpBackend, HttpParams } from '@angular/common/http';
import { PaginationParams } from './../../shared/common/paginationParams';
import { map, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { PostDto } from 'src/app/shared/models/post/postDto';
import { Post } from 'src/app/shared/models/post/post';
import { PaginatedResult } from 'src/app/shared/common/paginatedResult';

@Injectable({
  providedIn: 'root'
})
export class PostApiService {
  baseUrl = environment.apiUrl;
  private httpBackend: HttpClient;

  constructor(private http: HttpClient,
              private handler: HttpBackend) { 
                this.httpBackend = new HttpClient(handler);
              }

  getAllposts(paginationParams:PaginationParams) {
    let params = PaginationHeader.getPaginationHeader(paginationParams.page, paginationParams.pageSize);
    return this.getPaginatedResult<PostDto[]>(this.baseUrl + "posts", params);
  }

  getPostsByCategory(categoryId:string) : Observable<PostDto[]> {
    return this.http.get<PostDto[]>(this.baseUrl + "posts/obter-noticias-por-categoria/" + categoryId);
  }

  getPostById(postId:string) : Observable<PostDto>{
    return this.http.get<PostDto>(this.baseUrl + "posts/obter-noticia-por-id/" + postId);
  }

  create(post:Post) : Observable<Post> {
    return this.http.post<Post>(this.baseUrl + "posts/nova-noticia", post);
  }

  update(postId:string, post:Post) : Observable<any> {
    return this.http.put<any>(this.baseUrl + "posts/actualizar-noticia/" + postId, post);
  }

  remove(postId:string) : Observable<any> {
    return this.http.delete<any>(this.baseUrl + "posts/remover-noticia/" + postId);
  }

  getPaginatedResult<T>(url:string, params: HttpParams)  {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
    return this.http.get<T>(url, {observe: 'response', params}).pipe(
      map(response => {
        paginatedResult.result = response.body!
        if(response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')!);
        }
        
        return paginatedResult;
      })
    );
  }

  uploadImage(image:FormData) {
    return this.httpBackend.post<string>(this.baseUrl + "posts/enviar-imagem", image, {reportProgress : true, observe: 'events'});
  }
 
}
