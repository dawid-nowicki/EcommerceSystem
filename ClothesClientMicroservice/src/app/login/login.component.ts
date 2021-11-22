import { Component } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  private API_URL = environment.API_URL;
  public client: HttpClient;
  public registerForm!: FormGroup;
  private formBuilder: FormBuilder;
  public submitted = false;
  public showModal!: boolean;
  public loginError = false;
  public loginErrorMessage!: string;

  constructor(client: HttpClient, formBuilder: FormBuilder) {
    this.client = client;
    this.formBuilder = formBuilder;
  }
  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  login() {
    this.submitted = true;
    this.loginError = false;
    if (this.registerForm.invalid) {
      return;
    }
    if (this.submitted) {
      this.showModal = false;
    }
    this.client.post<Token>(this.API_URL + 'login', {
      "email": this.f.email.value,
      "password": this.f.password.value
    }).subscribe(x => {
      if (x['token']) {
        localStorage.setItem('token', x['token']);
        window.location.href = '/';
      } else {
        this.loginError = true;
        this.loginErrorMessage = x['message'];
      }
    })
  }
  get f() { return this.registerForm.controls; }
}
interface Token {
  token: string;
  message: string;
}
