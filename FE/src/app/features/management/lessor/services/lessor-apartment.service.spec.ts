import { TestBed } from '@angular/core/testing';

import { LessorApartmentService } from './lessor-apartment.service';

describe('LessorApartmentService', () => {
  let service: LessorApartmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LessorApartmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
