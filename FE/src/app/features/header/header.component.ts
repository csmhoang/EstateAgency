import {
  AfterViewInit,
  Component,
  ElementRef,
  inject,
  Input,
  Renderer2,
  ViewChild,
} from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements AfterViewInit {
  @Input()
  underline?: number;

  @ViewChild('navmenu', { read: ElementRef })
  navmenu!: ElementRef;

  renderer = inject(Renderer2);

  ngAfterViewInit(): void {
    if (this.underline) {
      const router = this.navmenu!.nativeElement.querySelector(
        `ul > li:nth-child(${this.underline}) > a`
      );
      if (router) {
        this.renderer.addClass(router, 'active');
      }
    }
  }
}
