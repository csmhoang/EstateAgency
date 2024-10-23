import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorPostComponent } from './lessor-post.component';

describe('LessorPostComponent', () => {
  let component: LessorPostComponent;
  let fixture: ComponentFixture<LessorPostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorPostComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
