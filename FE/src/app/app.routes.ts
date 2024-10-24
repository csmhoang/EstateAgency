import { Routes } from '@angular/router';
import { LoginComponent } from '@core/auth/pages/login/login.component';
import { RegisterComponent } from '@core/auth/pages/register/register.component';
import { ApartmentDetailComponent } from '@features/apartment/pages/apartment-detail/apartment-detail.component';
import { ApartmentComponent } from '@features/apartment/pages/apartment/apartment.component';
import { ContactComponent } from '@features/contact/pages/contact/contact.component';
import { HomeComponent } from '@features/home/pages/home/home.component';
import { LessorDetailComponent } from '@features/lessor/pages/lessor-detail/lessor-detail.component';
import { LessorComponent } from '@features/lessor/pages/lessor/lessor.component';
import { MaintenanceHistoryComponent } from '@features/maintenance/components/maintenance-history/maintenance-history.component';
import { LessorDashboardComponent } from '@features/management/lessor/pages/lessor-dashboard/lessor-dashboard.component';
import { LessorManagementComponent } from '@features/management/lessor/pages/lessor-management/lessor-management.component';
import { ProfileEditComponent } from '@features/profiles/pages/profile-edit/profile-edit.component';
import { RentedHistoryComponent } from '@features/profiles/pages/rented-history/rented-history.component';
import { ProfileComponent } from '@features/profiles/pages/profile/profile.component';
import { ServiceDetailComponent } from '@features/service/pages/service-detail/service-detail.component';
import { ServiceComponent } from '@features/service/pages/service/service.component';
import { ProfileActionsComponent } from '@features/profiles/pages/profile-actions/profile-actions.component';
import { LessorApartmentComponent } from '@features/management/lessor/pages/lessor-apartment/lessor-apartment.component';
import { PostFormComponent } from '@features/post/components/post-form/post-form.component';
import { ApartmentFormComponent } from '@features/apartment/components/apartment-form/apartment-form.component';
import { LessorPostComponent } from '@features/management/lessor/pages/lessor-post/lessor-post.component';
import { isLandlord, isUserAuthenticated } from '@core/guards/auth.guard';

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
      },
      {
        path: 'history',
        component: RentedHistoryComponent,
      },
      {
        path: 'maintenance',
        component: MaintenanceHistoryComponent,
      },
      {
        path: 'actions',
        component: ProfileActionsComponent,
      },
    ],
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
    path: 'agency/detail',
    component: LessorDetailComponent,
  },
  {
    path: 'agency',
    component: LessorComponent,
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
    path: 'lessor',
    component: LessorManagementComponent,
    canActivate: [isLandlord],
    canActivateChild: [isLandlord],
    children: [
      {
        path: '',
        component: LessorDashboardComponent,
      },
      {
        path: 'apartment/insert',
        component: ApartmentFormComponent,
      },
      {
        path: 'apartment',
        component: LessorApartmentComponent,
      },
      {
        path: 'post/insert',
        component: PostFormComponent,
      },
      {
        path: 'post',
        component: LessorPostComponent,
      },
    ],
  },

  /*Admin*/
  {
    path: '**',
    redirectTo: '/',
  },
];
