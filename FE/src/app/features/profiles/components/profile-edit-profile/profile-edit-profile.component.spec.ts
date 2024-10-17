import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileEditProfileComponent } from './profile-edit-profile.component';

describe('ProfileEditProfileComponent', () => {
  let component: ProfileEditProfileComponent;
  let fixture: ComponentFixture<ProfileEditProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileEditProfileComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProfileEditProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
