import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostRootListComponent } from './post-root-list.component';

describe('PostRootListComponent', () => {
  let component: PostRootListComponent;
  let fixture: ComponentFixture<PostRootListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PostRootListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PostRootListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
