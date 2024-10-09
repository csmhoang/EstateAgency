import { Routes } from '@angular/router';
import { LoginComponent } from '@core/auth/pages/login/login.component';
import { RegisterComponent } from '@core/auth/pages/register/register.component';
import { ApartmentDetailComponent } from '@features/apartment/pages/apartment-detail/apartment-detail.component';
import { ApartmentComponent } from '@features/apartment/pages/apartment/apartment.component';
import { ContactComponent } from '@features/contact/pages/contact/contact.component';
import { HomeComponent } from '@features/home/pages/home/home.component';
import { LessorDetailComponent } from '@features/lessor/pages/lessor-detail/lessor-detail.component';
import { LessorComponent } from '@features/lessor/pages/lessor/lessor.component';
import { ServiceDetailComponent } from '@features/service/pages/service-detail/service-detail.component';
import { ServiceComponent } from '@features/service/pages/service/service.component';

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


  /*Admin*/
];
