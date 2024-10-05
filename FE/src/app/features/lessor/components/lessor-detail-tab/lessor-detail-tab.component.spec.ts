import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorDetailTabComponent } from './lessor-detail-tab.component';

describe('LessorDetailTabComponent', () => {
  let component: LessorDetailTabComponent;
  let fixture: ComponentFixture<LessorDetailTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorDetailTabComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorDetailTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
