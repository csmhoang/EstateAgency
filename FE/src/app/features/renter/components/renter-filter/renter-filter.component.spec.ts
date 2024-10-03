import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenterFilterComponent } from './renter-filter.component';

describe('RenterFilterComponent', () => {
  let component: RenterFilterComponent;
  let fixture: ComponentFixture<RenterFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RenterFilterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RenterFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
