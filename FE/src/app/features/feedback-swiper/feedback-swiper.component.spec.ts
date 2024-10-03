import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeedbackSwiperComponent } from './feedback-swiper.component';

describe('FeedbackSwiperComponent', () => {
  let component: FeedbackSwiperComponent;
  let fixture: ComponentFixture<FeedbackSwiperComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FeedbackSwiperComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FeedbackSwiperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
