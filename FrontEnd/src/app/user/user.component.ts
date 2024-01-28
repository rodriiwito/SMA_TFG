import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserTypePipe } from '../authorization/util/user-type.pipe';
import {
  FormBuilder,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Country } from '../common/country/country.model';
import { UserType } from '../services/models/enums/user-type';
import { CountryComponent } from '../common/country/country.component';
import { UserService } from '../services/user/user.service';
import { first, switchMap, tap } from 'rxjs';
import { Router } from '@angular/router';
import { LoginComponent } from '../authorization/login/login.component';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, UserTypePipe],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css',
})
export class UserComponent implements OnInit {
  errorMessage: string | null = null;
  countries: Country[];
  email: string = '';

  dataUserType: UserType[] = Object.keys(UserType)
    .map((k) => parseInt(k))
    .filter((k) => !isNaN(k));

  form = this.formBuilder.group({
    email: ['', [Validators.required]],
    country: ['', [Validators.required]],
    userType: [-1, [Validators.min(0)]],
    organization: [''],
  });

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private countryComponent: CountryComponent,
    private router: Router,
    private loginComponent: LoginComponent
  ) {
    this.countries = this.countryComponent.getCountryList();
  }
  ngOnInit() {
    this.userService
      .getUser()
      .pipe(
        first(),
        tap((user) => {
          this.form.setValue({
            email: user.email,
            country: user.country,
            userType: user.userType,
            organization: user.organization,
          });
          this.form.controls['email'].disable();
        })
      )
      .subscribe();
  }
  update() {
    const value = this.form.value;
    if (this.form.valid && value.country) {
      this.userService
        .updateUser({
          country: value.country,
          organization: value.organization ?? null,
          userType: value.userType ?? 0,
        })
        .pipe(first())
        .subscribe();
    }
  }
  deleteUser() {
    this.userService
      .deleteUser()
      .pipe(first())
      .subscribe((_) => {
        this.loginComponent.logout();
        this.router.navigateByUrl('/login');
      });
  }
}
