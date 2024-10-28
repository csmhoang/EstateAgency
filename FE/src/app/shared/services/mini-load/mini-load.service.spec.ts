import { TestBed } from '@angular/core/testing';

import { MiniLoadService } from './mini-load.service';

describe('MiniLoadService', () => {
  let service: MiniLoadService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MiniLoadService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
