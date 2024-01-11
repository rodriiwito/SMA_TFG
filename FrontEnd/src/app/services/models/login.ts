import { BaseResponse } from './base-response';

export interface LoginRequest {
  userName: string;
  password: string;
}

export interface LoginResponse extends BaseResponse {
  token: string;
  expiry: string;
}
