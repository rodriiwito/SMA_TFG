import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  createUrlTreeFromSnapshot,
} from '@angular/router';
import { LoginComponent } from './authorization/login/login.component';
import { first, map } from 'rxjs';

export const verify = (next: ActivatedRouteSnapshot) => {
  return inject(LoginComponent)
    .verifyEmail(next.params['id'])
    .pipe(
      first(),
      map((_) => {
        return createUrlTreeFromSnapshot(next, ['/', 'login']);
      })
    );
};
