import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenterPageComponent } from './renter-page.component';

describe('RenterPageComponent', () => {
  let component: RenterPageComponent;
  let fixture: ComponentFixture<RenterPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RenterPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RenterPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
