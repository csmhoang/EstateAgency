import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlockAccountComponent } from './block-account.component';

describe('BlockAccountComponent', () => {
  let component: BlockAccountComponent;
  let fixture: ComponentFixture<BlockAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BlockAccountComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BlockAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
