import { BaseResponse } from './base-response';

export interface RegisterRequest {
  email: string;
  password: string;
}

export interface RegisterResponse extends BaseResponse {}
