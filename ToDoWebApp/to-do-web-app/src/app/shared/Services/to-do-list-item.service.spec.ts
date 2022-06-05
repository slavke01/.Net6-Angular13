import { TestBed } from '@angular/core/testing';

import { ToDoListItemService } from './to-do-list-item.service';

describe('ToDoListItemService', () => {
  let service: ToDoListItemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ToDoListItemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
