import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorDetailComponent } from './lessor-detail.component';

describe('LessorDetailComponent', () => {
  let component: LessorDetailComponent;
  let fixture: ComponentFixture<LessorDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorDetailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
