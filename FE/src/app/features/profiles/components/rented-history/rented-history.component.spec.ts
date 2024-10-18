import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentedHistoryComponent } from './rented-history.component';

describe('RentedHistoryComponent', () => {
  let component: RentedHistoryComponent;
  let fixture: ComponentFixture<RentedHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RentedHistoryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RentedHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
