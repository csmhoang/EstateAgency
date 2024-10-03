import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApartmentDetailPageComponent } from './apartment-detail-page.component';

describe('ApartmentDetailPageComponent', () => {
  let component: ApartmentDetailPageComponent;
  let fixture: ComponentFixture<ApartmentDetailPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApartmentDetailPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApartmentDetailPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
