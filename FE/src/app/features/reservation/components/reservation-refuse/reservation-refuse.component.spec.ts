import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationRefuseComponent } from './reservation-refuse.component';

describe('ReservationRefuseComponent', () => {
  let component: ReservationRefuseComponent;
  let fixture: ComponentFixture<ReservationRefuseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationRefuseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReservationRefuseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
