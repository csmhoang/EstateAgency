import { Routes } from '@angular/router';
import { RegisterFormComponent } from '@core/auth/components/register-form/register-form.component';
import { LoginComponent } from '@core/auth/pages/login/login.component';
import { RegisterComponent } from '@core/auth/pages/register/register.component';
import { ApartmentDetailComponent } from '@features/apartment/pages/apartment-detail/apartment-detail.component';
import { ApartmentComponent } from '@features/apartment/pages/apartment/apartment.component';
import { ContactComponent } from '@features/contact/pages/contact/contact.component';
import { LessorDashboardComponent } from '@features/dashboard/lessor/pages/lessor-dashboard/lessor-dashboard.component';
import { HomeComponent } from '@features/home/pages/home/home.component';
import { LessorDetailComponent } from '@features/lessor/pages/lessor-detail/lessor-detail.component';
import { LessorComponent } from '@features/lessor/pages/lessor/lessor.component';
import { ProfileEditComponent } from '@features/profiles/components/profile-edit/profile-edit.component';
import { ProfileComponent } from '@features/profiles/pages/profile/profile.component';
import { ServiceDetailComponent } from '@features/service/pages/service-detail/service-detail.component';
import { ServiceComponent } from '@features/service/pages/service/service.component';
import path from 'path';

export const routes: Routes = [
  /*Clients*/
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'profile',
    component: ProfileComponent,
    children: [
      {
        path: '',
        component: ProfileEditComponent,
        outlet: 'profile',
      },
    ],
  },
  {
    path: 'lessor/detail',
    component: LessorDetailComponent,
  },
  {
    path: 'lessor',
    component: LessorComponent,
  },
  {
    path: 'service/detail',
    component: ServiceDetailComponent,
  },
  {
    path: 'service',
    component: ServiceComponent,
  },
  {
    path: 'apartment/detail',
    component: ApartmentDetailComponent,
  },
  {
    path: 'apartment',
    component: ApartmentComponent,
  },
  {
    path: 'contact',
    component: ContactComponent,
  },
  /*Lessors*/
  {
    path: 'lessor/login',
    component: LoginComponent,
  },
  {
    path: 'lessor/register',
    component: RegisterComponent,
  },
  {
    path: 'lessor/dashboard',
    component: LessorDashboardComponent,
    children: [
      {
        path: '',
        component: RegisterFormComponent,
        outlet: 'lessor',
      },
    ],
  },

  /*Admin*/
  {
    path: 'admin/login',
    component: LoginComponent,
  },
  {
    path: 'admin/register',
    component: RegisterComponent,
  },
  {
    path: '**',
    redirectTo: '/',
  },
];
