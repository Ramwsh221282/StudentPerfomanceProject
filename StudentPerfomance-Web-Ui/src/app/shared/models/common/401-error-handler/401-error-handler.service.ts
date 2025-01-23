import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../../../../pages/user-page/services/auth.service';

@Injectable({ providedIn: 'root' })
export class UnauthorizedErrorHandler {
  public constructor(
    private readonly _router: Router,
    private readonly _auth: AuthService,
  ) {}

  public tryHandle(error: HttpErrorResponse): void {
    if (error.status != 401) return;
    this._auth.setNotAuthorized();
    this._router.navigate(['login']);
  }
}
