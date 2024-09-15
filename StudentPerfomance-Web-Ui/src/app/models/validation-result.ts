export class ValidationResult {
  private readonly _message: string;
  private _isError: boolean;

  public constructor(message: string, isError: boolean) {
    this._message = message;
    this._message.length == 0
      ? (this._isError = false)
      : (this._isError = true);
  }

  public get Message(): string {
    return this._message;
  }

  public get IsError(): boolean {
    return this._isError;
  }
}
