import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorReservationComponent } from './lessor-reservation.component';

describe('LessorReservationComponent', () => {
  let component: LessorReservationComponent;
  let fixture: ComponentFixture<LessorReservationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorReservationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorReservationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
