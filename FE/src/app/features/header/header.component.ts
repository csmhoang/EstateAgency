import {
  AfterViewInit,
  Component,
  ElementRef,
  inject,
  Input,
  OnInit,
  PLATFORM_ID,
  Renderer2,
  ViewChild,
} from '@angular/core';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { filter, zip } from 'rxjs';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  // nav: ElementRef | undefined;

  // @ViewChild('navmenu', { read: ElementRef })
  // navmenu: ElementRef | undefined;

  // renderer = inject(Renderer2);
  // router = inject(Router);

  // ngOnInit(): void {
  //   this.router.events
  //     .pipe(filter((event) => event instanceof NavigationEnd))
  //     .subscribe(() => {
  //       const routers: { [key: string]: number } = {
  //         '/': 1,
  //         '/apartment': 2,
  //         '/lessor': 3,
  //         '/service': 4,
  //         '/contact': 5,
  //       };
  //       const underline: number | undefined = routers[this.router.url];
  //       if (this.nav) {
  //         this.renderer.removeClass(this.nav, 'active');
  //       }
  //       if (underline) {
  //         this.nav = this.navmenu?.nativeElement.querySelector(
  //           `ul > li:nth-child(${underline}) > a`
  //         );
  //         if (this.nav) {
  //           this.renderer.addClass(this.nav, 'active');
  //         }
  //       }
  //     });
  // }
}
