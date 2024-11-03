import { Component, OnInit } from '@angular/core';
import { AssignmentSessionPaginationService } from '../assignment-session-pagination.service';

@Component({
  selector: 'app-assignment-sessions-pagination',
  templateUrl: './assignment-sessions-pagination.component.html',
  styleUrl: './assignment-sessions-pagination.component.scss',
})
export class AssignmentSessionsPaginationComponent implements OnInit {
  public constructor(
    private readonly service: AssignmentSessionPaginationService
  ) {}

  public ngOnInit(): void {}
}
