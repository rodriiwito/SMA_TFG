import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  createUrlTreeFromSnapshot,
} from '@angular/router';
import { LoginComponent } from './authorization/login/login.component';

export const authGuard = (next: ActivatedRouteSnapshot) => {
  return inject(LoginComponent).isLoggedIn()
    ? true
    : createUrlTreeFromSnapshot(next, ['/', 'login']);
};
