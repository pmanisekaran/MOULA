import { TestBed } from '@angular/core/testing';

import { PaymentDataService } from './payment-service.service';

describe('PaymentServiceService', () => {
  let service: PaymentDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PaymentDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
