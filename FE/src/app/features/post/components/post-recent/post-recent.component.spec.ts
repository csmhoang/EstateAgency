import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostRecentComponent } from './post-recent.component';

describe('PostRecentComponent', () => {
  let component: PostRecentComponent;
  let fixture: ComponentFixture<PostRecentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PostRecentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PostRecentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
