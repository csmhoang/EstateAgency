import { Component } from '@angular/core';
import { LessorItemComponent } from '../lessor-item/lessor-item.component';

@Component({
  selector: 'app-lessor-list',
  standalone: true,
  imports: [LessorItemComponent],
  templateUrl: './lessor-list.component.html',
  styleUrl: './lessor-list.component.scss'
})
export class LessorListComponent {

}
