import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoxChatComponent } from './box-chat.component';

describe('BoxChatComponent', () => {
  let component: BoxChatComponent;
  let fixture: ComponentFixture<BoxChatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoxChatComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BoxChatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
