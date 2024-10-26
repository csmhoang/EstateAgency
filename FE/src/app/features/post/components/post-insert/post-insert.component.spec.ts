import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostInsertComponent } from './post-insert.component';

describe('PostInsertComponent', () => {
  let component: PostInsertComponent;
  let fixture: ComponentFixture<PostInsertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PostInsertComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PostInsertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
