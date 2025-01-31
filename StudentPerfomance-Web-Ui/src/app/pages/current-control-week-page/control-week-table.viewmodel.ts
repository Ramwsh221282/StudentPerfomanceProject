import { Injectable } from '@angular/core';
import { AssignmentSession } from '../../modules/administration/submodules/assignment-sessions/models/assignment-session-interface';

@Injectable({
  providedIn: 'any',
})
export class CurrentControlWeekViewModel {
  private _session: AssignmentSession;
  private _isInited: boolean = false;

  public initialize(session: AssignmentSession): void {
    if (this._isInited) return;
    this._session = { ...session };
    this._isInited = true;
  }

  public get session(): AssignmentSession {
    return this._session;
  }

  public closeSession(): void {
    this._session = null!;
    this._isInited = false;
  }
}
