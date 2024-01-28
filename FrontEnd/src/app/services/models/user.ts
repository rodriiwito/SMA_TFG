import { UserType } from './enums/user-type';

export interface GetUser {
  email: string;
  country: string;
  organization: string;
  userType: UserType;
}
export interface UpdateUserRequest {
  country: string;
  organization: string | null;
  userType: UserType;
}
