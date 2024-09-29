import { Component, Input, OnInit } from '@angular/core';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { PostComponent } from '../post/post.component';

@Component({
  selector: 'app-list-post',
  standalone: true,
  imports: [NgbPaginationModule, PostComponent],
  templateUrl: './list-post.component.html',
  styleUrl: './list-post.component.scss',
})
export class ListPostComponent implements OnInit {
  @Input()
  page: number = 1;

  @Input()
  items: any;

  ngOnInit(): void {
    this.items = ['', '', ''];
  }
}
