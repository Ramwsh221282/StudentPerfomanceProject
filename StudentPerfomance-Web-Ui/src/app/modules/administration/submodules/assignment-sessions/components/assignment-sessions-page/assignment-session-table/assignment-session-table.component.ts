import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSession } from '../../../models/assignment-session-interface';
import { Router } from '@angular/router';

@Component({
    selector: 'app-assignment-session-table',
    templateUrl: './assignment-session-table.component.html',
    styleUrl: './assignment-session-table.component.scss',
    standalone: false
})
export class AssignmentSessionTableComponent {
  @Input({ required: true }) activeAssignmentSession: AssignmentSession | null;
  @Output() sessionCloseRequested: EventEmitter<AssignmentSession> =
    new EventEmitter();
  @Output() sessionCreated: EventEmitter<AssignmentSession> =
    new EventEmitter();

  protected isCreatingSession: boolean = false;

  public constructor(private readonly _router: Router) {}

  protected navigateToDocumentation(): void {
    const path = ['/assignment-sessions-info'];
    this._router.navigate(path);
  }
}
