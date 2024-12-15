import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'any',
})
export class UserOperationNotificationService {
  private _message: string;
  private _isSuccess: boolean;
  private _isFailure: boolean;

  constructor() {
    this._message = '';
    this._isSuccess = false;
    this._isFailure = false;
  }

  public get Message(): string {
    return this._message;
  }

  public set SetMessage(value: string) {
    this._message = value;
  }

  public get isSuccess(): boolean {
    return this._isSuccess;
  }

  public get isFailure(): boolean {
    return this._isFailure;
  }

  public reset(): void {
    this._isSuccess = false;
    this._isFailure = false;
    this._message = '';
  }

  public success(): void {
    this._isSuccess = true;
    this._isFailure = false;
  }

  public failure(): void {
    this._isSuccess = false;
    this._isFailure = true;
  }
}
