export class TransactionState {
  private readonly _state: boolean;
  private readonly _message: string;
  public constructor(state: boolean, message: string) {
    this._state = state;
    this._message = message;
  }
  public get State(): boolean {
    return this._state;
  }
  public get Message(): string {
    return this._message;
  }
}
