import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { UserClothesComponent } from './user-clothes/user-clothes.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AuthInterceptorService } from './auth-interceptor.service';
import { LoginComponent } from './login/login.component';
import { WeatherInspirationComponent } from './weather-inspiration/weather-inspiration.component';
import { FooterComponent } from './footer/footer.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';
import { RegisterComponent } from './register/register.component';
import { AboutComponent } from './about/about.component';


@NgModule({
  declarations: [
    AppComponent,
    UserClothesComponent,
    HomeComponent,
    NavMenuComponent,
    LoginComponent,
    WeatherInspirationComponent,
    FooterComponent,
    RegisterComponent,
    AboutComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AutocompleteLibModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'my-clothes', component: UserClothesComponent },
      { path: 'login', component: LoginComponent },
      { path: 'inspirations', component: WeatherInspirationComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'about', component: AboutComponent },
    ], { useHash: true })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
