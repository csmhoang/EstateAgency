import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileEditAccountComponent } from './profile-edit-account.component';

describe('ProfileEditAccountComponent', () => {
  let component: ProfileEditAccountComponent;
  let fixture: ComponentFixture<ProfileEditAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileEditAccountComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProfileEditAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
