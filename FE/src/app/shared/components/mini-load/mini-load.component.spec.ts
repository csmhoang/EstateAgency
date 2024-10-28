import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MiniLoadComponent } from './mini-load.component';

describe('MiniLoadComponent', () => {
  let component: MiniLoadComponent;
  let fixture: ComponentFixture<MiniLoadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MiniLoadComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MiniLoadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
