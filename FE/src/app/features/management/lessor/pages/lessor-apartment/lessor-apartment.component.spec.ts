import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorApartmentComponent } from './lessor-apartment.component';

describe('LessorApartmentComponent', () => {
  let component: LessorApartmentComponent;
  let fixture: ComponentFixture<LessorApartmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorApartmentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorApartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
