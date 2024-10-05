import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApartmentPrimaryInfoComponent } from './apartment-primary-info.component';

describe('ApartmentPrimaryInfoComponent', () => {
  let component: ApartmentPrimaryInfoComponent;
  let fixture: ComponentFixture<ApartmentPrimaryInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApartmentPrimaryInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApartmentPrimaryInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
