import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorBookingComponent } from './lessor-booking.component';

describe('LessorBookingComponent', () => {
  let component: LessorBookingComponent;
  let fixture: ComponentFixture<LessorBookingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorBookingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorBookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
