import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessorPageComponent } from './lessor-page.component';

describe('LessorPageComponent', () => {
  let component: LessorPageComponent;
  let fixture: ComponentFixture<LessorPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessorPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LessorPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
