import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'any',
})
export class ModalDialogService {
  private _showDialog: boolean;

  constructor() {
    this._showDialog = false;
  }
  public get isVisible(): boolean {
    return this._showDialog;
  }

  public turnOn(): void {
    this._showDialog = true;
  }

  public turnOff(): void {
    this._showDialog = false;
  }

  public turn(value: boolean): void {
    this._showDialog = value;
  }
}
