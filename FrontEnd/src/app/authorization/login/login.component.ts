import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { first, tap } from 'rxjs';
import { LoginResponse } from 'src/app/services/models';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      email: [
        '',
        [Validators.required, Validators.email, Validators.maxLength(320)],
      ],
      password: ['', Validators.required],
    });
  }

  login() {
    const val = this.form.value;

    if (val.email && val.password) {
      this.authService
        .login({ password: val.password, userName: val.email })
        .pipe(
          first(),
          tap((response) => this.setSession(response))
        )
        .subscribe(() => {
          this.router.navigateByUrl('/');
        });
    }
  }

  private setSession(authResult: LoginResponse) {
    localStorage.setItem('id_token', authResult.token);
    localStorage.setItem('expiry_token', authResult.expiry);
  }

  logout() {
    localStorage.removeItem('id_token');
    localStorage.removeItem('expiry_token');
  }

  public isLoggedIn() {
    let tokenExpiry = localStorage.getItem('expiry_token');
    return tokenExpiry !== null && new Date(tokenExpiry) > new Date();
  }

  isLoggedOut() {
    return !this.isLoggedIn();
  }

  register() {
    this.router.navigateByUrl('/register');
  }
}
