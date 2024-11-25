import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingRefuseComponent } from './booking-refuse.component';

describe('BookingRefuseComponent', () => {
  let component: BookingRefuseComponent;
  let fixture: ComponentFixture<BookingRefuseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookingRefuseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BookingRefuseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
