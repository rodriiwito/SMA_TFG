import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthInterceptorProviders } from './services/auth/auth-interceptor';
import { LoginComponent } from './authorization/login/login.component';
@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
  ],
  providers: [AuthInterceptorProviders, LoginComponent],
  bootstrap: [AppComponent],
})
export class AppModule {}
