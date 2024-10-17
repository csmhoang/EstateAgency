import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileEditOverviewComponent } from './profile-edit-overview.component';

describe('ProfileEditOverviewComponent', () => {
  let component: ProfileEditOverviewComponent;
  let fixture: ComponentFixture<ProfileEditOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileEditOverviewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProfileEditOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
