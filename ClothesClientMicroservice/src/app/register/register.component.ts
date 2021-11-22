import { Component } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators, ValidatorFn, ValidationErrors, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
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
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, { validator: this.confirmPasswordValidator() });
  }


  confirmPasswordValidator() {
    return (group: FormGroup) => {
      let passwordInput = group.controls['password'];
      let confirmPasswordInput = group.controls['confirmPassword'];
      if (passwordInput.value !== confirmPasswordInput.value) {
        confirmPasswordInput.setErrors({ notSame: true })
      } else {
        confirmPasswordInput.setErrors(null)
      }

    };
  }
  register() {
    this.submitted = true;
    this.loginError = false;
    if (this.registerForm.invalid) {
      return;
    }
    if (this.submitted) {
      this.showModal = false;
    }
    this.client.post<Token>(this.API_URL + 'register', {
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
