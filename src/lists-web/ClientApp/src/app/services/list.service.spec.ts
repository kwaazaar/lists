import { TestBed } from '@angular/core/testing';

import { HttpClientModule } from '@angular/common/http';

import { ListService } from './list.service';

describe('ListService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [ HttpClientModule ],
  }));

  it('should be created', () => {
    const service: ListService = TestBed.get(ListService);
    expect(service).toBeTruthy();
  });
});
