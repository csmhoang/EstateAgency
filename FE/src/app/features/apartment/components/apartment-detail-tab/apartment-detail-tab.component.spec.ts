import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApartmentDetailTabComponent } from './apartment-detail-tab.component';

describe('ApartmentDetailTabComponent', () => {
  let component: ApartmentDetailTabComponent;
  let fixture: ComponentFixture<ApartmentDetailTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApartmentDetailTabComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApartmentDetailTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
