import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApartmentInsertComponent } from './apartment-insert.component';

describe('ApartmentInsertComponent', () => {
  let component: ApartmentInsertComponent;
  let fixture: ComponentFixture<ApartmentInsertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApartmentInsertComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApartmentInsertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
