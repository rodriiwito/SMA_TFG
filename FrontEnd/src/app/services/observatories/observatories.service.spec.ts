import { TestBed } from '@angular/core/testing';

import { ObservatoriesService } from './observatories.service';

describe('ObservatoriesService', () => {
  let service: ObservatoriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ObservatoriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
