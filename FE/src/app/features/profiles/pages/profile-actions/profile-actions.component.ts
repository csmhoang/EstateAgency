import { Component, OnInit } from '@angular/core';
import { User } from '@core/models/user.model';
import { UserService } from '@core/services/user.service';

import { LessorListComponent } from '@features/lessor/components/lessor-list/lessor-list.component';
import { PostListComponent } from '@features/post/components/post-list/post-list.component';
import { Post } from '@features/post/models/post.model';

@Component({
  selector: 'app-profile-actions',
  standalone: true,
  imports: [LessorListComponent, PostListComponent],
  templateUrl: './profile-actions.component.html',
  styleUrl: './profile-actions.component.scss',
})
export class ProfileActionsComponent implements OnInit {
  user = this.userService.currentUser();
  followees?: User[];
  savedPosts?: Post[];
  constructor(private userService: UserService) {}

  ngOnInit() {
    this.followees = this.user?.followees?.map(
      (followee) => followee.followee!
    );
    this.savedPosts = this.user?.savePosts?.map((savePost) => savePost.post!);
  }
}
