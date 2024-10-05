import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostRootItemComponent } from './post-root-item.component';

describe('PostRootItemComponent', () => {
  let component: PostRootItemComponent;
  let fixture: ComponentFixture<PostRootItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PostRootItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PostRootItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
