import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorInfoCardComponent } from './lessor-info-card.component';

describe('LessorInfoCardComponent', () => {
  let component: LessorInfoCardComponent;
  let fixture: ComponentFixture<LessorInfoCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorInfoCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorInfoCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
