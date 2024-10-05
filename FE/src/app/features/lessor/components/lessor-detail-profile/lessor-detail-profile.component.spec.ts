import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorDetailProfileComponent } from './lessor-detail-profile.component';

describe('LessorDetailProfileComponent', () => {
  let component: LessorDetailProfileComponent;
  let fixture: ComponentFixture<LessorDetailProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorDetailProfileComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorDetailProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
