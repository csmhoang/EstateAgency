import { Routes } from '@angular/router';
import { LoginComponent } from '@core/auth/pages/login/login.component';
import { RegisterComponent } from '@core/auth/pages/register/register.component';
import { ApartmentDetailComponent } from '@features/apartment/pages/apartment-detail/apartment-detail.component';
import { ApartmentComponent } from '@features/apartment/pages/apartment/apartment.component';
import { ContactComponent } from '@features/contact/pages/contact/contact.component';
import { HomeComponent } from '@features/home/pages/home/home.component';
import { LessorDetailComponent } from '@features/lessor/pages/lessor-detail/lessor-detail.component';
import { LessorComponent } from '@features/lessor/pages/lessor/lessor.component';
import { LessorDashboardComponent } from '@features/management/lessor/pages/lessor-dashboard/lessor-dashboard.component';
import { LessorManagementComponent } from '@features/management/lessor/pages/lessor-management/lessor-management.component';
import { ProfileEditComponent } from '@features/profiles/pages/profile-edit/profile-edit.component';
import { ProfileComponent } from '@features/profiles/pages/profile/profile.component';
import { ServiceComponent } from '@features/service/pages/service/service.component';
import { ProfileActionsComponent } from '@features/profiles/pages/profile-actions/profile-actions.component';
import { LessorApartmentComponent } from '@features/management/lessor/pages/lessor-apartment/lessor-apartment.component';
import { LessorPostComponent } from '@features/management/lessor/pages/lessor-post/lessor-post.component';
import { ApartmentInsertComponent } from '@features/apartment/components/apartment-insert/apartment-insert.component';
import { PostInsertComponent } from '@features/post/components/post-insert/post-insert.component';
import { apartmentDetailResolver } from '@features/apartment/resolver/apartment-detail.resolver';
import { VerifyEmailComponent } from '@core/auth/pages/verify-email/verify-email.component';
import { VerifyResetPasswordComponent } from '@core/auth/pages/verify-reset-password/verify-reset-password.component';
import { lessorDetailResolver } from '@features/lessor/resolver/lessor-detail.resolver';
import {
  isAdmin,
  isLandlord,
  isUserAuthenticated,
} from '@core/guards/auth.guard';
import { ReservationListComponent } from '@features/reservation/components/reservation-list/reservation-list.component';
import { BookingListComponent } from '@features/booking/components/booking-list/booking-list.component';
import { LessorReservationComponent } from '@features/management/lessor/pages/lessor-reservation/lessor-reservation.component';
import { LessorBookingComponent } from '@features/management/lessor/pages/lessor-booking/lessor-booking.component';
import { LessorProfileComponent } from '@features/management/lessor/pages/lessor-profile/lessor-profile.component';
import { CartComponent } from '@features/Cart/pages/cart/cart.component';
import { RentedApartmentComponent } from '@features/apartment/components/rented-apartment/rented-apartment.component';
import { AdminDashboardComponent } from '@features/management/admin/pages/admin-dashboard/admin-dashboard.component';
import { AdminManagementComponent } from '@features/management/admin/pages/admin-management/admin-management.component';
import { AdminPostComponent } from '@features/management/admin/pages/admin-post/admin-post.component';
import { AdminAccountComponent } from '@features/management/admin/pages/admin-account/admin-account.component';

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
    path: 'verify-email',
    component: VerifyEmailComponent,
  },
  {
    path: 'verify-reset-password',
    component: VerifyResetPasswordComponent,
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [isUserAuthenticated],
    children: [
      {
        path: '',
        component: ProfileEditComponent,
      },
      {
        path: 'booking',
        component: BookingListComponent,
      },
      {
        path: 'reservation',
        component: ReservationListComponent,
      },
      {
        path: 'rented-apartment',
        component: RentedApartmentComponent,
      },
      {
        path: 'actions',
        component: ProfileActionsComponent,
      },
    ],
  },
  {
    path: 'cart',
    canActivate: [isUserAuthenticated],
    component: CartComponent,
  },
  {
    path: 'service',
    component: ServiceComponent,
  },
  {
    path: 'agency/detail/:id',
    component: LessorDetailComponent,
    resolve: {
      lessor: lessorDetailResolver,
    },
  },
  {
    path: 'agency',
    component: LessorComponent,
  },
  {
    path: 'apartment/detail/:id',
    component: ApartmentDetailComponent,
    resolve: {
      post: apartmentDetailResolver,
    },
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
    children: [
      {
        path: '',
        component: LessorDashboardComponent,
      },
      {
        path: 'apartment/insert',
        component: ApartmentInsertComponent,
      },
      {
        path: 'apartment',
        component: LessorApartmentComponent,
      },
      {
        path: 'post/insert',
        component: PostInsertComponent,
      },
      {
        path: 'post',
        component: LessorPostComponent,
      },
      {
        path: 'reservation',
        component: LessorReservationComponent,
      },
      {
        path: 'booking',
        component: LessorBookingComponent,
      },
    ],
  },
  {
    path: 'lessor/profile',
    canActivate: [isLandlord],
    component: LessorProfileComponent,
    children: [
      {
        path: '',
        component: ProfileEditComponent,
      },
      {
        path: 'actions',
        component: ProfileActionsComponent,
      },
    ],
  },

  /*Admin*/
  {
    path: 'admin',
    component: AdminManagementComponent,
    canActivate: [isAdmin],
    children: [
      {
        path: '',
        component: AdminDashboardComponent,
      },
      {
        path: 'post',
        component: AdminPostComponent,
      },
      {
        path: 'account',
        component: AdminAccountComponent,
      },
    ],
  },
  {
    path: 'admin/profile',
    canActivate: [isAdmin],
    component: LessorProfileComponent,
    children: [
      {
        path: '',
        component: ProfileEditComponent,
      },
      {
        path: 'actions',
        component: ProfileActionsComponent,
      },
    ],
  },
  /*Default*/
  {
    path: '**',
    redirectTo: '/',
  },
];
