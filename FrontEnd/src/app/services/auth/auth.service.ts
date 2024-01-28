import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  BaseResponse,
  LoginRequest,
  LoginResponse,
  RegisterRequest,
  RegisterResponse,
} from '../models';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private url: string = 'http://localhost:5257/api/auth';

  constructor(private http: HttpClient) {}

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.url}/login`, request);
  }

  register(request: RegisterRequest): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(`${this.url}/register`, request);
  }

  verifyEmail(token: string): Observable<BaseResponse> {
    return this.http.post<BaseResponse>(
      `${this.url}/verify-email/${token}`,
      {}
    );
  }
}
