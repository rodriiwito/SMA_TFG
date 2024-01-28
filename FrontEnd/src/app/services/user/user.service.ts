import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GetUser, UpdateUserRequest } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private url: string = 'http://localhost:5257/api/user';

  constructor(private http: HttpClient) {}

  deleteUser(): Observable<void> {
    return this.http.delete<void>(`${this.url}`);
  }

  getUser(): Observable<GetUser> {
    return this.http.get<GetUser>(`${this.url}`);
  }

  updateUser(request: UpdateUserRequest): Observable<void> {
    return this.http.post<void>(`${this.url}/update`, request);
  }
}
