import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './authorization/login/login.component';
import { authGuard } from './auth-guard';
import { NavbarComponent } from './navbar/navbar.component';
import { RegisterComponent } from './authorization/register/register.component';
import { verify } from './verify-guard';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'verify-email/:id',
    canActivate: [verify],
    children: [],
  },

  {
    path: '',
    canActivate: [authGuard],
    component: NavbarComponent,
    loadChildren: () =>
      import('./navbar/routes').then((mod) => mod.USER_ROUTES),
  },
  { path: '**', redirectTo: '' },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
