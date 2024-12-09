import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentedApartmentComponent } from './rented-apartment.component';

describe('RentedApartmentComponent', () => {
  let component: RentedApartmentComponent;
  let fixture: ComponentFixture<RentedApartmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RentedApartmentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RentedApartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
