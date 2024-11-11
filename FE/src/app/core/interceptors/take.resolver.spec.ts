import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { takeResolver } from './take.resolver';

describe('takeResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => takeResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
