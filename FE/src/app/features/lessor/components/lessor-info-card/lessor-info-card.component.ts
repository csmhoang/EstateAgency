import { CommonModule } from '@angular/common';
import { Component, computed, Input } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { User } from '@core/models/user.model';
import { PresenceService } from '@core/services/presence.service';

@Component({
  selector: 'app-lessor-info-card',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './lessor-info-card.component.html',
  styleUrl: './lessor-info-card.component.scss',
})
export class LessorInfoCardComponent {
  @Input() landlord?: User;

  isFollow = new FormControl(false);

  isOnline = computed(() =>
    this.presenceService.onlineUsers().includes(this.landlord?.username!)
  );

  constructor(private presenceService: PresenceService) {}
}
