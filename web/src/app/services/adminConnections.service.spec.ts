/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AdminConnectionsService } from './adminConnections.service';

describe('Service: AdminConnections', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AdminConnectionsService]
    });
  });

  it('should ...', inject([AdminConnectionsService], (service: AdminConnectionsService) => {
    expect(service).toBeTruthy();
  }));
});
