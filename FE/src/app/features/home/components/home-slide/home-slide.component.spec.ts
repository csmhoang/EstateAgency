import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeSlideComponent } from './home-slide.component';

describe('HomeSlideComponent', () => {
  let component: HomeSlideComponent;
  let fixture: ComponentFixture<HomeSlideComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomeSlideComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HomeSlideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
