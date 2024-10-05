import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorFilterComponent } from './lessor-filter.component';

describe('LessorFilterComponent', () => {
  let component: LessorFilterComponent;
  let fixture: ComponentFixture<LessorFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorFilterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
