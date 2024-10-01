import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ServiceComponent } from './features/service/service.component';
import { ServiceDetailComponent } from './features/service/components/service-detail/service-detail.component';
import { ApartmentComponent } from './features/apartment/apartment.component';

export const routes: Routes = [
  // {
  //   path: '',
  //   component: HomeComponent,
  // },
  // {
  //   path: '',
  //   component: ServiceComponent,
  // },
  // {
  //   path: '',
  //   component: ServiceDetailComponent,
  // },
  {
    path: '',
    component: ApartmentComponent,
  },
];
