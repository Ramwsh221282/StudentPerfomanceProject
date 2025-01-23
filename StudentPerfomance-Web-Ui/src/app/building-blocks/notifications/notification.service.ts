import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'any',
})
export class NotificationService {
  private _isSuccess: boolean = false;
  private _isFailure: boolean = false;
  private _isVisible: boolean = false;
  private _message: string = '';

  public success(): void {
    this._isSuccess = true;
    this._isFailure = false;
  }

  public failure(): void {
    this._isFailure = true;
    this._isSuccess = false;
  }

  public turn(): void {
    this._isVisible = !this._isVisible;
  }

  public isVisible(): boolean {
    return this._isVisible;
  }

  public setMessage(message: string): void {
    this._message = message;
  }

  public message(): string {
    return this._message;
  }

  public isSuccess(): boolean {
    return this._isSuccess;
  }

  public isFailure(): boolean {
    return this._isFailure;
  }

  public bulkFailure(message: string): void {
    this.setMessage(message);
    this.failure();
    this.turn();
  }

  public bulkSuccess(message: string): void {
    this.setMessage(message);
    this.success();
    this.turn();
  }
}
