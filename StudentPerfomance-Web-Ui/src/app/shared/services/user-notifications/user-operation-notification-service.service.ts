import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'any',
})
export class UserOperationNotificationService {
  private _message: string;

  constructor() {
    this._message = '';
  }

  public get Message(): string {
    return this._message;
  }

  public set SetMessage(value: string) {
    this._message = value;
  }
}
