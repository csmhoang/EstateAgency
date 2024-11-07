import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { apartmentDetailResolver } from './apartment-detail.resolver';

describe('apartmentDetailResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => apartmentDetailResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
