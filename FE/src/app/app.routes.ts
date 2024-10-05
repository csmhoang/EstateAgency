import { Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { ServicePageComponent } from './pages/service-page/service-page.component';
import { ServiceDetailPageComponent } from './pages/service-detail-page/service-detail-page.component';
import { ApartmentPageComponent } from './pages/apartment-page/apartment-page.component';
import { ContactPageComponent } from './pages/contact-page/contact-page.component';
import { ApartmentDetailPageComponent } from './pages/apartment-detail-page/apartment-detail-page.component';
import { LessorDetailPageComponent } from './pages/lessor-detail-page/lessor-detail-page.component';
import { LessorPageComponent } from './pages/lessor-page/lessor-page.component';

export const routes: Routes = [
  {
    path: '',
    component: HomePageComponent,
  },
  {
    path: 'lessor/detail',
    component: LessorDetailPageComponent,
  },
  {
    path: 'lessor',
    component: LessorPageComponent,
  },
  {
    path: 'service/detail',
    component: ServiceDetailPageComponent,
  },
  {
    path: 'service',
    component: ServicePageComponent,
  },
  {
    path: 'apartment/detail',
    component: ApartmentDetailPageComponent,
  },
  {
    path: 'apartment',
    component: ApartmentPageComponent,
  },
  {
    path: 'contact',
    component: ContactPageComponent,
  },
];
