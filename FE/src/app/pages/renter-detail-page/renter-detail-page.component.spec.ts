import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenterDetailPageComponent } from './renter-detail-page.component';

describe('RenterDetailPageComponent', () => {
  let component: RenterDetailPageComponent;
  let fixture: ComponentFixture<RenterDetailPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RenterDetailPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RenterDetailPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
