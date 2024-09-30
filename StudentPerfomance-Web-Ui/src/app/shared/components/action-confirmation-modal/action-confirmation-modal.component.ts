export abstract class ActionConfirmationModalComponent {
  protected actionResultValue: boolean = false;

  public constructor() {}

  protected confirm() {
    this.actionResultValue = true;
    this.close();
  }

  protected decline() {
    this.actionResultValue = false;
    this.close();
  }

  protected abstract close(): void;
}
