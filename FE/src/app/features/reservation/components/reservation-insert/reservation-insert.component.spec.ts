import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationInsertComponent } from './reservation-insert.component';

describe('ReservationInsertComponent', () => {
  let component: ReservationInsertComponent;
  let fixture: ComponentFixture<ReservationInsertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationInsertComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReservationInsertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
