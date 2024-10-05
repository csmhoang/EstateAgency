import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApartmentRelationshipComponent } from './apartment-relationship.component';

describe('ApartmentRelationshipComponent', () => {
  let component: ApartmentRelationshipComponent;
  let fixture: ComponentFixture<ApartmentRelationshipComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApartmentRelationshipComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApartmentRelationshipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
