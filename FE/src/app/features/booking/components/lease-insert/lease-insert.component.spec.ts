import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaseInsertComponent } from './lease-insert.component';

describe('LeaseInsertComponent', () => {
  let component: LeaseInsertComponent;
  let fixture: ComponentFixture<LeaseInsertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeaseInsertComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LeaseInsertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
