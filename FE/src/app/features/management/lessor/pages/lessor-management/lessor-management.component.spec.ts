import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorManagementComponent } from './lessor-management.component';

describe('LessorManagementComponent', () => {
  let component: LessorManagementComponent;
  let fixture: ComponentFixture<LessorManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorManagementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
