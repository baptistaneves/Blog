import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateAdminUser } from 'src/app/shared/models/user/createAdminUser';
import { CreatePublicUser } from 'src/app/shared/models/user/createPublicUser';
import { Login } from 'src/app/shared/models/user/login';
import { UpdateUserProfile } from 'src/app/shared/models/user/updateUserProfile';
import { UserProfile } from 'src/app/shared/models/user/userProfile';
import { User } from 'src/app/shared/models/user/user';
import { UserRole } from 'src/app/shared/models/user/userRole';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAdminUsers(): Observable<UserProfile[]> {
    return this.http.get<UserProfile[]>(this.baseUrl + "userProfile/obter-usuarios-admins")
  }
  
  getPublicUsers(): Observable<UserProfile[]> {
    return this.http.get<UserProfile[]>(this.baseUrl + "userProfile/obter-usuarios-publicos")
  }

  getUserRoles(): Observable<UserRole[]> {
    return this.http.get<UserRole[]>(this.baseUrl + "identity/obter-tipos-de-usuarios")
  }

  getUserById(userId: string): Observable<UserProfile> {
    return this.http.get<UserProfile>(this.baseUrl + "userProfile/obter-usuario-por-id/" + userId);
  }

  getCurrentUser(): Observable<User> {
    return this.http.get<User>(this.baseUrl + "identity/obter-usuario-logado");
  }

  createAdminUser(user: CreateAdminUser): Observable<User> {
    return this.http.post<User>(this.baseUrl + "identity/novo-usuario", user);
  }

  createPublicUser(user: CreatePublicUser): Observable<User> {
    return this.http.post<User>(this.baseUrl + "identity/registar-se", user);
  }

  updateUserProfile(identityId:string, userUpdated: UpdateUserProfile): Observable<any> {
    return this.http.patch<any>(this.baseUrl + "userProfile/actualizar-usuario/" + identityId, userUpdated);
  }

  removeUser(userId: string): Observable<any> {
    return this.http.delete<any>(this.baseUrl + "identity/remover-usuario/" + userId);
  }

  deleteAccount(userId: string): Observable<any> {
    return this.http.delete<any>(this.baseUrl + "identity/excluir-minha-conta/" + userId);
  }
  
  login(login: Login): Observable<User> {
    return this.http.post<User>(this.baseUrl + "identity/login", login);
  }

  logout(): Observable<any> {
    return this.http.get<any>(this.baseUrl + "identity/logout");
  }

}
