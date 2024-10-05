import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorItemComponent } from './lessor-item.component';

describe('LessorItemComponent', () => {
  let component: LessorItemComponent;
  let fixture: ComponentFixture<LessorItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
