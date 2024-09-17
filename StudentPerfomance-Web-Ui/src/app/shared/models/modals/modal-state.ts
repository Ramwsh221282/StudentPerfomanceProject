import { signal, WritableSignal } from '@angular/core';

export class ModalState {
  private _visible: WritableSignal<boolean>;
  public constructor() {
    this._visible = signal<boolean>(false);
  }
  public get Visibility(): boolean {
    return this._visible();
  }

  public turnOff(): void {
    this._visible.set(false);
  }

  public turnOn(): void {
    this._visible.set(true);
  }

  public turn(hidden: boolean): void {
    this._visible.set(hidden);
  }
}
