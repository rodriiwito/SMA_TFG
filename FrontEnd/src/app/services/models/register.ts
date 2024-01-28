import { BaseResponse } from './base-response';
import { UserType } from './enums/user-type';

export interface RegisterRequest {
  email: string;
  password: string;
  userType: UserType;
  country: string;
  organization: string | null;
}

export interface RegisterResponse extends BaseResponse {}
