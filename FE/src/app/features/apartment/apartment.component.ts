import { Component } from '@angular/core';
import { ScrollTopComponent } from '../../shared/components/scroll-top/scroll-top.component';
import { FooterComponent } from '../footer/footer.component';
import { HeaderComponent } from '../header/header.component';
import { PostItemComponent } from '../post/components/post-item/post-item.component';

@Component({
  selector: 'app-apartment',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    ScrollTopComponent,
    PostItemComponent,
  ],
  templateUrl: './apartment.component.html',
  styleUrl: './apartment.component.scss',
})
export class ApartmentComponent {}
