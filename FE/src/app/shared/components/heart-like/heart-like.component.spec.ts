import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeartLikeComponent } from './heart-like.component';

describe('HeartLikeComponent', () => {
  let component: HeartLikeComponent;
  let fixture: ComponentFixture<HeartLikeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HeartLikeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HeartLikeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
