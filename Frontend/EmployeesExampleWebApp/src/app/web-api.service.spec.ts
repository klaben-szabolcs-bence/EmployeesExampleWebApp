import { TestBed } from '@angular/core/testing';

import { WebAPIService } from './web-api.service';

describe('WebAPIService', () => {
  let service: WebAPIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WebAPIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
