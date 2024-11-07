import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { User } from '@core/models/user.model';

@Component({
  selector: 'app-lessor-info-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lessor-info-card.component.html',
  styleUrl: './lessor-info-card.component.scss',
})
export class LessorInfoCardComponent {
  @Input() landlord!: User;
}
