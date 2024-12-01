import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorLeaseViewComponent } from './lessor-lease-view.component';

describe('LessorLeaseViewComponent', () => {
  let component: LessorLeaseViewComponent;
  let fixture: ComponentFixture<LessorLeaseViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorLeaseViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorLeaseViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
