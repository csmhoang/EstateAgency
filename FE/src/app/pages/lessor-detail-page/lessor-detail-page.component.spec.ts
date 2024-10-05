import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorDetailPageComponent } from './lessor-detail-page.component';

describe('LessorDetailPageComponent', () => {
  let component: LessorDetailPageComponent;
  let fixture: ComponentFixture<LessorDetailPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorDetailPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorDetailPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
