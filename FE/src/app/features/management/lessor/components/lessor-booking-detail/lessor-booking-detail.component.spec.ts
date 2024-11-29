import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorBookingDetailComponent } from './lessor-booking-detail.component';

describe('LessorBookingDetailComponent', () => {
  let component: LessorBookingDetailComponent;
  let fixture: ComponentFixture<LessorBookingDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorBookingDetailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorBookingDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
