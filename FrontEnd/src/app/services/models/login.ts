import { BaseResponse } from './base-response';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse extends BaseResponse {
  token: string;
  expiry: string;
}
