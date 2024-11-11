import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { lessorDetailResolver } from './lessor-detail.resolver';

describe('lessorDetailResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => lessorDetailResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
