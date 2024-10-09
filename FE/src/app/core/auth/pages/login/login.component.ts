import { Component } from '@angular/core';
import { LoginFormComponent } from '@core/auth/components/login-form/login-form.component';
import { HeaderComponent } from '@core/layout/header/header.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [HeaderComponent, LoginFormComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {}
