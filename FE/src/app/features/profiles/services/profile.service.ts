import { DestroyRef, inject, Injectable, signal } from '@angular/core';
import { User } from '@core/models/user.model';
import { UserService } from '@core/services/user.service';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  user = signal<User | null>(null);

  constructor(private userService: UserService) {}
}
