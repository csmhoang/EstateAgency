import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingInsertComponent } from './booking-insert.component';

describe('BookingInsertComponent', () => {
  let component: BookingInsertComponent;
  let fixture: ComponentFixture<BookingInsertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookingInsertComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BookingInsertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
