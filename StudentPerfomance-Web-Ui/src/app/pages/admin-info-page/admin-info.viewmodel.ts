import { Injectable } from '@angular/core';
import { InfoTheme } from './admin-info-page-section-item-interface';

@Injectable({
  providedIn: 'any',
})
export class AdminInfoViewmodel {
  public topic: InfoTheme | null;

  public selectTopic(topic: InfoTheme): void {
    this.topic = topic;
  }

  public get currentTopic(): InfoTheme | null {
    return this.topic;
  }

  public isCurrentTopic(topic: InfoTheme): boolean {
    if (this.topic == null) return false;
    return this.topic.title == topic.title;
  }
}
