import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApartmentFilterComponent } from './apartment-filter.component';

describe('ApartmentFilterComponent', () => {
  let component: ApartmentFilterComponent;
  let fixture: ComponentFixture<ApartmentFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApartmentFilterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApartmentFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
