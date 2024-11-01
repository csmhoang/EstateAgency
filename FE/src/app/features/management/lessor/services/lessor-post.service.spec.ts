import { TestBed } from '@angular/core/testing';

import { LessorPostService } from './lessor-post.service';

describe('LessorPostService', () => {
  let service: LessorPostService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LessorPostService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
