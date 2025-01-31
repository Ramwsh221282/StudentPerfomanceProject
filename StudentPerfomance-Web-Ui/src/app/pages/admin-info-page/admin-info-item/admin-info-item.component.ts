import { Component, Input } from '@angular/core';
import { NgClass, NgForOf } from '@angular/common';
import {
  AdminInfoPageSectionItem,
  InfoTheme,
} from '../admin-info-page-section-item-interface';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';
import { AdminInfoViewmodel } from '../admin-info.viewmodel';

@Component({
  selector: 'app-admin-info-item',
  imports: [NgForOf, NgClass],
  templateUrl: './admin-info-item.component.html',
  styleUrl: './admin-info-item.component.scss',
  standalone: true,
  animations: [
    trigger('expandCollapse', [
      state(
        'false',
        style({
          height: '0',
          opacity: 0,
          overflow: 'hidden',
        }),
      ),
      state(
        'true',
        style({
          height: '*',
          opacity: 1,
          overflow: 'visible',
        }),
      ),
      transition('false => true', [animate('300ms ease-out')]),
      transition('true => false', [animate('300ms ease-in')]),
    ]),
  ],
})
export class AdminInfoItemComponent {
  @Input({ required: true }) infoItem: AdminInfoPageSectionItem;
  public isShown: boolean = false;

  constructor(protected readonly viewModel: AdminInfoViewmodel) {}

  public selectTopic(topic: InfoTheme, $event: MouseEvent): void {
    $event.stopPropagation();
    this.viewModel.selectTopic(topic);
  }

  public showTopics($event: MouseEvent): void {
    $event.preventDefault();
    this.isShown = !this.isShown;
  }
}
