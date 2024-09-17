export interface INotificationMessageBuilder<T> {
  buildMessage(parameter: T): string;
}
