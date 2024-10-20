import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { skipResolver } from './skip.resolver';

describe('skipResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => skipResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
