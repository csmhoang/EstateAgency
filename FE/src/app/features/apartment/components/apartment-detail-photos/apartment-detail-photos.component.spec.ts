import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApartmentDetailPhotosComponent } from './apartment-detail-photos.component';

describe('ApartmentDetailPhotosComponent', () => {
  let component: ApartmentDetailPhotosComponent;
  let fixture: ComponentFixture<ApartmentDetailPhotosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApartmentDetailPhotosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApartmentDetailPhotosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
