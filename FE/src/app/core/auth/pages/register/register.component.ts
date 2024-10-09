import { Component } from '@angular/core';
import { RegisterFormComponent } from '@core/auth/components/register-form/register-form.component';
import { HeaderComponent } from '@core/layout/header/header.component';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [HeaderComponent, RegisterFormComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {

}
