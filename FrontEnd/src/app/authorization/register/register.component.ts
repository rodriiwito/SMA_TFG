import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  AbstractControl,
  FormBuilder,
  FormsModule,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Router } from '@angular/router';
import { CountryComponent } from 'src/app/common/country/country.component';
import { Country } from 'src/app/common/country/country.model';
import { UserType } from 'src/app/services/models/enums/user-type';
import { UserTypePipe } from '../util/user-type.pipe';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, UserTypePipe],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  errorMessage: string | null = null;
  countries: Country[];

  dataUserType: UserType[] = Object.keys(UserType)
    .map((k) => parseInt(k))
    .filter((k) => !isNaN(k));

  form = this.formBuilder.group(
    {
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      repeatPassword: ['', [Validators.required]],
      country: ['', [Validators.required]],
      userType: [-1, [Validators.min(0)]],
      organization: [''],
    },
    {
      validators: [matchPassword],
    }
  );

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private countryComponent: CountryComponent
  ) {
    this.countries = this.countryComponent.getCountryList();
  }

  register() {
    const value = this.form.value;
    if (this.form.valid && value.password && value.email && value.country) {
      this.authService
        .register({
          password: value.password,
          email: value.email,
          country: value.country,
          organization: value.organization ?? null,
          userType: value.userType ?? 0,
        })
        .subscribe((response) => {
          if (!response.error) {
            this.router.navigateByUrl('/');
          } else if (response.message) {
            this.errorMessage = response.message;
          }
        });
    }
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
