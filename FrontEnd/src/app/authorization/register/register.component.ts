import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  AbstractControl,
  AbstractControlOptions,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  form = this.formBuilder.group(
    {
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      repeatPassword: ['', [Validators.required]],
    },
    {
      validators: [matchPassword],
    }
  );

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  register() {
    const value = this.form.value;
    if (this.form.valid && value.password && value.email)
      this.authService
        .register({
          password: value.password,
          email: value.email,
        })
        .subscribe((response) => {
          if (!response.error) {
            this.router.navigateByUrl('/');
          }
        });
  }
}

export const matchPassword: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  let password = control.get('password');
  let confirmPassword = control.get('repeatPassword');
  if (password?.value != confirmPassword?.value) {
    return {
      passwordMatchError: true,
    };
  }

  return null;
};
